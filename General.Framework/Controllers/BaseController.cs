using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Entities;
using General.Services.Category;
using General.Core;

namespace General.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        private AjaxResult _ajaxResult;

        public BaseController()
        {
            this._ajaxResult = new AjaxResult();
        }
        /// <summary>
        /// ajax请求的数据结果
        /// </summary>
        public AjaxResult AjaxData
        {
            get { return _ajaxResult; }
        }
    }
}
