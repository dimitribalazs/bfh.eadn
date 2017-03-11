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

        public ActionResult Details(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        public ActionResult Create(Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Answer answer)
        {
            _service.Validation(ModelState, answer);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(answer);
                    return RedirectToAction("Edit", "Question", new { id = answer.QuestionId });
                }
                catch
                {
                    //todo log stuff
                }
            }
            return View(answer);
        }

        public ActionResult Edit(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Answer answer)
        {
            _service.Validation(ModelState, answer);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, answer);
                    return RedirectToAction("Edit", new { id = answer.QuestionId });
                }
                catch
                {
                    //todo log stuff
                }
            }
            return View(answer);
        }

        public ActionResult Delete(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Answer answer)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Edit", "Question", new { id = answer.QuestionId });
            }
            catch
            {
                return View(answer);
            }
        }
    }
}
