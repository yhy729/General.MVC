using General.Services.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Menu.Register
{
    public class RegisterApplicationService : IRegisterApplicationService
    {
        private ICategoryService _categoryService;
        public RegisterApplicationService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitRegister()
        {
            var list = new List<Entities.Category>();
            FunctionManager.GetFunctionLists().ForEach(item =>
            {
                list.Add(new Entities.Category
                {
                    Action = item.Action,
                    Controller = item.Controller,
                    CssClass = item.CssClass,
                    FatherResource = item.FatherResource,
                    IsMenu = item.IsMenu,
                    Name = item.Name,
                    RouteName = item.RouteName,
                    SysResource = item.SysResource,
                    Sort = item.Sort,
                    FatherId = item.FatherId,
                    ResourceId = item.ResourceId,
                    IsDisabled = false
                });
            });
            _categoryService.InitCategory(list);
        }
    }
}
