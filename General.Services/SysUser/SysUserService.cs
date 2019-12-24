using General.Core.Data;
using General.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using General.Core.Librs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService
    {
        private IMemoryCache _memoryCache;
        private const string MODEL_KEY = "General.Services.SysUser_{0}";
        private IRepository<Entities.SysUser> _sysUserRepository;
        private IRepository<Entities.SysUserToken> _sysUserTokenRepository;
        public SysUserService(IRepository<Entities.SysUser> sysUserRepository
            , IMemoryCache memoryCache
            , IRepository<Entities.SysUserToken> sysUserTokenRepository)
        {
            this._sysUserRepository = sysUserRepository;
            this._memoryCache = memoryCache;
            this._sysUserTokenRepository = sysUserTokenRepository;
        }

        /// <summary>
        /// 验证登录状态
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <param name="r">登录随机数</param>
        /// <returns></returns>
        public (bool Status, string Message, string Token, Entities.SysUser User) ValidateUser(string account, string password, string r)
        {
            var user = GetByAccount(account);
            if (user == null)
            {
                return (false, "用户名或密码错误", null, null);
            }

            if (!user.Enabled)
            {
                return (false, "该账号已被冻结", null, null);
            }

            if (user.LoginLock)
            {
                if (user.AllowLoginTime.HasValue && user.AllowLoginTime > DateTime.Now)
                {
                    var waitMin = (user.AllowLoginTime - DateTime.Now).Value.Minutes + 1;
                    return (false, "您的账号已被锁定，请您" + waitMin + "分钟后再使用", null, null);
                }
            }

            var md5Password = EncryptorHelper.Md5(user.Password + r);
            if (password.Equals(md5Password, StringComparison.InvariantCultureIgnoreCase))
            {
                user.LoginLock = false;
                user.LoginFailedNum = 0;
                user.AllowLoginTime = null;
                user.LastLoginTime = DateTime.Now;
                user.LastIpAddress = "";
                //登陆日志
                user.SysUserLoginLogs.Add(new SysUserLoginLog
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登陆成功"
                });

                //TODO单点登录移除旧的Token

                var userToken = new SysUserToken
                {
                    Id = Guid.NewGuid(),
                    ExpireTime = DateTime.Now.AddDays(15)
                };
                user.SysUserTokens.Add(userToken);
                _sysUserRepository.DbContext.SaveChanges();

                return (true, "登录成功", userToken.Id.ToString(), user);
            }
            else
            {
                //登陆日志
                user.SysUserLoginLogs.Add(new SysUserLoginLog
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登陆密码错误"
                });
                user.LoginFailedNum++;
                if (user.LoginFailedNum > 5)
                {
                    user.LoginLock = true;
                    user.AllowLoginTime = DateTime.Now.AddMinutes(15);
                }
                _sysUserRepository.DbContext.SaveChanges();
            }
            return (false, "用户名或密码错误", null, null);
        }

        /// <summary>
        /// 通过登陆号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Entities.SysUser GetByAccount(string account)
        {
            return _sysUserRepository.Table.FirstOrDefault(x => x.Account == account && !x.IsDeleted);
        }

        /// <summary>
        /// 通过当前登录用户的token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Entities.SysUser GetLogged(string token)
        {
            //先检查缓存中是否存在
            if (_memoryCache.TryGetValue(token, out SysUserToken userToken))
            {
                if (userToken != null)
                {
                    if (_memoryCache.TryGetValue(string.Format(MODEL_KEY, userToken.SysUserId), out Entities.SysUser sysUser))
                    {
                        if (sysUser != null)
                        {
                            return sysUser;
                        }
                    }
                }
            }

            //缓存中没有就从数据库取 然后缓存下来
            Guid tokenId = Guid.Empty;
            if (Guid.TryParse(token, out tokenId))
            {
                var tokenItem = _sysUserTokenRepository.Table
                    .Include(x => x.SysUser)
                    .FirstOrDefault(x => x.Id == tokenId);
                if (tokenItem != null)
                {
                    //缓存起来
                    _memoryCache.Set(token, tokenItem, DateTimeOffset.Now.AddHours(4));
                    _memoryCache.Set(string.Format(MODEL_KEY, tokenItem.SysUserId), tokenItem.SysUser, DateTimeOffset.Now.AddHours(4));
                    return tokenItem.SysUser;
                }
            }
            return null;
        }
    }
}
