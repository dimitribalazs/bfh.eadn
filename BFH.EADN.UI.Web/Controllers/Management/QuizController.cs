using BFH.EADN.UI.Web.Models.Management;
using System;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class QuizController : Controller
    {
        private Services.QuizService _service = new Services.QuizService();
        // GET: Quiz
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Quiz/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        // GET: Quiz/Create
        public ActionResult Create()
        {
            return View(_service.Get());
        }

        // POST: Quiz/Create
        [HttpPost]
        public ActionResult Create(Quiz quiz)
        {
            try
            {
                // TODO: Add insert logic here
                _service.Create(quiz);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(quiz);
            }
        }

        // GET: Quiz/Edit/5
        public ActionResult Edit(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Quiz quiz)
        {
            try
            {
                _service.Edit(id, quiz);
                return RedirectToAction("Details", new { id = id });
            }
            catch(Exception ex)
            {
                Quiz dbQuiz = _service.Get(id);
                dbQuiz.Text = quiz.Text;
                return View(dbQuiz);
            }
        }

        // GET: Quiz/Delete/5
        public ActionResult Delete(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Quiz quiz)
        {
            try
            {
                // TODO: Add delete logic here
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
