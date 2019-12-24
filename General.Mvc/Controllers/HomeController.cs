using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Mvc.Models;
using General.Entities;
using General.Services.Category;
using General.Core;

namespace General.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;
        //public HomeController(ICategoryService categoryService)
        //{
        //    this._categoryService = categoryService;
        //}
        public IActionResult Index()
        {
            _categoryService = EnginContext.Current.Resolve<ICategoryService>();
             var list = _categoryService.GetAll();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
