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

        public ActionResult Play(Guid quizId, Guid? questionId)
        {
            if(questionId.HasValue == false)
            {
                return View(_service.GetFirstQuestion(quizId));
            }
            return View(_service.GetQuestion(quizId, questionId.Value));
        }

        public ActionResult Completed(Guid quizId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Check(Guid quizId, Guid questionId, List<Guid> answers, Guid? nextQuestionId)
        {
            if (answers == null || answers.Count == 0)
            {
                ModelState.AddModelError("answers", "To progress you must solve this question");
            }
            else if (_service.CheckAnswers(questionId, answers) == false)
            {
                ModelState.AddModelError("answers", "Wrong answers");
            }

            if(ModelState.IsValid)
            {
                if(nextQuestionId.HasValue)
                { 
                    return RedirectToAction("Play", new { quizId = quizId, questionId = nextQuestionId });
                }
                return RedirectToAction("Completed", new { quizId = quizId });
            }
            return View("Play", _service.GetQuestion(quizId, questionId));
        }
    }
}