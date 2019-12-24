using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using General.Entities;
using General.Framework.Infrastructure;
using General.Framework.Menu;
using General.Framework.Security.Admin;

namespace General.Framework
{
    public class WorkContext : IWorkContext
    {
        private IAuthenticationService _authenticationService;
        public WorkContext(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// 当前登陆用户
        /// </summary>
        public SysUser CurrentUser
        {
            get { return _authenticationService.GetCurrentUser(); }
        }

        /// <summary>
        /// 当前登录用户的菜单
        /// </summary>
        public List<Category> Categories
        {
            get
            {
                return FunctionManager.GetFunctionLists().Select(item => new Category
                {
                    Name = item.Name,
                    Action = item.Name,
                    Controller = item.Controller,
                    CssClass = item.CssClass,
                    FatherId = item.FatherId,
                    FatherResource = item.FatherResource,
                    IsMenu = item.IsMenu,
                    ResourceId = item.ResourceId,
                    RouteName = item.RouteName,
                    Sort = item.Sort,
                    SysResource = item.SysResource
                }).ToList();
            }
        }
    }
}
