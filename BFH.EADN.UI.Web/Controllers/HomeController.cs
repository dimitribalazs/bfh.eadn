using BFH.EADN.UI.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var x = 0;
            //var y = 10 / x;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}