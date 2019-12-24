using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.SysUser
{
    public interface ISysUserService
    {
        /// <summary>
        /// 验证登录状态
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <param name="r">登录随机数</param>
        /// <returns></returns>
        (bool Status, string Message, string Token, Entities.SysUser User) ValidateUser(string account, string password, string r);

        /// <summary>
        /// 通过登陆号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Entities.SysUser GetByAccount(string account);

        /// <summary>
        /// 通过当前登录用户的token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Entities.SysUser GetLogged(string token);
    }
}
