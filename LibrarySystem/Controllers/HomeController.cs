using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "图书馆智能辅助剔旧系统";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "图书馆智能辅助剔旧系统";

            return View();
        }
    }
}