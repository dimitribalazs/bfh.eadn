using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class QuestionController : Controller
    {
        private QuestionService _service = new QuestionService();
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        public ActionResult Create()
        {
            return View(_service.Get());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(question);
                    return RedirectToAction("Index");
                }
                catch
                {

                }
            }
            return View(_service.Get());
        }

        public ActionResult Edit(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Question question)
        {
            try
            {
                _service.Edit(id, question);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Question question)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(question);
            }
        }
    }
}
