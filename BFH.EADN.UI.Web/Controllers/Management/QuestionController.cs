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

        // GET: Questions/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View(_service.Get());
        }

        // POST: Questions/Create
        [HttpPost]
        public ActionResult Create(Question question)
        {
            try
            {
                // TODO: Add insert logic here
                _service.Create(question);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
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

        // GET: Questions/Delete/5
        public ActionResult Delete(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Question question)
        {
            try
            {
                // TODO: Add delete logic here
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
