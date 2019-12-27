using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    public class MainController : PublicAdminController
    {
        private IAuthenticationService _authenticationService;
        public MainController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            //var user = WorkContext.CurrentUser;
            var user = _authenticationService.GetCurrentUser();
            return View();
        }
    }
}