using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using General.Framework.Security.Admin;

namespace General.Framework.Filters
{
    /// <summary>
    /// 登陆状态过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthFilter : Attribute, IResourceFilter
    {
        private IAuthenticationService _authenticationService;

        public AdminAuthFilter(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var user = _authenticationService.GetCurrentUser();
            if (user == null || !user.Enabled)
            {
                context.Result = new RedirectToActionResult("Index", "Login", new { Area = "admin", returnUrl = context.HttpContext.Request.Path });
            }
        }
    }
}
