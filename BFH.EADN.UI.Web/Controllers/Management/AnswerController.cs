using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class AnswerController : Controller
    {
        private AnswerService _service = new AnswerService();
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Answer/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        // GET: Answer/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: Answer/Create
        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            try
            {
                _service.Create(answer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(answer);
            }
        }

        // GET: Answer/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_service.Get(id));
        }

        // POST: Answer/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Answer answer)
        {
            try
            {
                _service.Edit(id, answer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(answer);
            }
        }

        // GET: Answer/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_service.Get(id));
        }

        // POST: Answer/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Answer answer)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(answer);
            }
        }
    }
}
