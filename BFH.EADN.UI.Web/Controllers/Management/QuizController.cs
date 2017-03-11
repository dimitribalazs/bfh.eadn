using BFH.EADN.UI.Web.Models.Management;
using System;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class QuizController : Controller
    {
        private Services.QuizService _service = new Services.QuizService();

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
        public ActionResult Create(Quiz quiz)
        {
            if (quiz.MinQuestionCount > quiz.MaxQuestionCount)
            {
                ModelState.AddModelError("MinQuestionCount", "Cannot be bigger than MaxQuestionCount");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(quiz);
                    return RedirectToAction("Index");
                }
                catch
                {
                    
                }
            }
            return View(quiz);
        }

        public ActionResult Edit(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Quiz quiz)
        {
            if (quiz.MinQuestionCount > quiz.MaxQuestionCount)
            {
                ModelState.AddModelError("MinQuestionCount", "Cannot be bigger than MaxQuestionCount");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, quiz);
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {

                }
            }
            Quiz dbQuiz = _service.Get(id);
            dbQuiz.Text = quiz.Text;
            return View(dbQuiz);
        }

        public ActionResult Delete(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Quiz quiz)
        {
            try
            { 
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(quiz);
            }
        }
    }
}
