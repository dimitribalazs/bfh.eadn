using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers
{
    /// <summary>
    /// Display Error pages
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Default error without error code
        /// </summary>
        /// <returns>error "Error" view</returns>
        public ActionResult Default()
        {
            ViewBag.ErrorMessage = "There happened an error";
            return View("Error");
        }

        /// <summary>
        /// Error with error code
        /// </summary>
        /// <returns>error "Error" view</returns>
        public ActionResult Code(int code)
        {
            ViewBag.ErrorMessage = "There happened an error " + code;
            return View("Error");
        }
    }
}