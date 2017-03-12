using BFH.EADN.UI.Web.Attributes;
using BFH.EADN.UI.Web.Utils;
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
            HttpContext.Session.GetSessionContext();
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