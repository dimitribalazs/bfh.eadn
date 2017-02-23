using BFH.EADN.UI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Play
{
    public class PlayController : Controller
    {
        private PlayService _service = new PlayService();
        public ActionResult Index()
        {
            return View(_service.GetOverview());
        }
    }
}