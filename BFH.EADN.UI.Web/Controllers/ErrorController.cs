using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Default()
        {
            ViewBag.ErrorMessage = "There happened an error";
            return View("Error");
        }

        public ActionResult Code(int code)
        {
            ViewBag.ErrorMessage = "There happened an error " + code;
            return View("Error");
        }
    }
}