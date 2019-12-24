using System;
using System.Collections.Generic;
using System.Text;
using General.Entities;

namespace General.Framework.Security.Admin
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        void SignIn(string token, string name);

        /// <summary>
        /// 退出登录
        /// </summary>
        void SignOut();

        /// <summary>
        /// 获取当前登陆用户（缓存）
        /// </summary>
        /// <returns></returns>
        SysUser GetCurrentUser();
    }
}
