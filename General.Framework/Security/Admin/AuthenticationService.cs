using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using General.Entities;
using General.Services.SysUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace General.Framework.Security.Admin
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISysUserService _sysUserService;
        public AuthenticationService(IHttpContextAccessor httpContextAccessor,
            ISysUserService sysUserService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 获取当前登陆用户（缓存）
        /// </summary>
        /// <returns></returns>
        public SysUser GetCurrentUser()
        {
            var result = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAdminAuthInfo.AuthenticationScheme).Result;
            if (result.Principal == null)
            {
                return null;
            }
            var token = result.Principal.FindFirst(ClaimTypes.Sid).Value;
            return _sysUserService.GetLogged(token ?? "");
        }

        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        public void SignIn(string token, string name)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, token));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _httpContextAccessor.HttpContext.SignInAsync(CookieAdminAuthInfo.AuthenticationScheme, claimsPrincipal);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAdminAuthInfo.AuthenticationScheme);
        }
    }
}
