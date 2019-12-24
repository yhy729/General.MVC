using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理登陆控制器
    /// </summary>
    public class LoginController : AdminAreaController
    {
        private const string R_KEY = "R_KEY";
        private ISysUserService _sysUserService;
        private IAuthenticationService _authenticationService;
        public LoginController(ISysUserService sysUserService, IAuthenticationService authenticationService)
        {
            this._sysUserService = sysUserService;
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// 页面--登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            string r = Core.Librs.EncryptorHelper.Md5(Guid.NewGuid().ToString());
            HttpContext.Session.SetString(R_KEY, r);
            var model = new LoginModel
            {
                R = r
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Logon(LoginModel model)
        {
            string r = HttpContext.Session.GetString(R_KEY) ?? "";
            if (!ModelState.IsValid)
            {
                AjaxData.Message = "请输入用户账号和密码";
                return Json(AjaxData);
            }
            var result = _sysUserService.ValidateUser(model.Account, model.Password, r);
            AjaxData.Status = result.Status;
            AjaxData.Message = result.Message;
            if (result.Status)
            {
                //保存登录状态
                _authenticationService.SignIn(result.Token, result.User.Name);
            }
            return Json(AjaxData);
        }

        /// <summary>
        /// 获取密码盐
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public IActionResult GetSalt(string account)
        {
            var user = _sysUserService.GetByAccount(account);
            return Content(user?.Salt);
        }

        public IActionResult SignOut()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Index");
        }
    }
}