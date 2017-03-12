using BFH.EADN.UI.Web.Models.Play;
using BFH.EADN.UI.Web.Services;
using BFH.EADN.UI.Web.Utils;
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

        [HttpGet]
        public ActionResult Index()
        {
            List<Overview> overview = _service.GetOverview();
            HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            if (cookie != null)
            {
                overview.First().ContinueQuizUrl = HttpUtility.UrlDecode(cookie.Values["Url"]);
                bool evalAtEnd;
                if(bool.TryParse(cookie.Values["EvaluationAtEnd"], out evalAtEnd))
                {
                    HttpContext.Session.GetSessionContext().EvaluationAtEnd = evalAtEnd;
                }
            }
            else
            {
                cookie = new HttpCookie("QuizState");
            }
            HttpContext.Response.Cookies.Set(cookie);
            return View(overview);
        }

        [HttpGet]
        public ActionResult ValidationType(Guid quizId)
        {
            HttpContext.Session.GetSessionContext().CurrentQuiz = _service.GetContractQuiz(quizId);
            ValidationType type = new ValidationType();
            type.QuizId = quizId;
            type.QuestionId = _service.GetFirstQuestion(quizId).QuestionId;
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationType(ValidationType type)
        {
            HttpContext.Session.GetSessionContext().EvaluationAtEnd = type.EvaluationAtEnd;

            HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            cookie.Values["EvaluationAtEnd"] = type.EvaluationAtEnd.ToString();
            return RedirectToAction("Play", new { quizId = type.QuizId, questionId = type.QuestionId });
        }

        [HttpGet]
        public ActionResult Play(Guid quizId, Guid? questionId)
        {
            //update cookie with new url
            HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            cookie.Values["Url"] = HttpUtility.UrlEncode(HttpContext.Request.Url.AbsoluteUri);
            HttpContext.Response.Cookies.Remove("QuizState");
            HttpContext.Response.Cookies.Set(cookie);
            
            return View(_service.GetQuestion(quizId, questionId.Value));
        }

        [HttpGet]
        public ActionResult Completed(Guid quizId)
        {
            bool evaluateAtTheEnd = HttpContext.Session.GetSessionContext().EvaluationAtEnd;
            if (evaluateAtTheEnd)
            {
                //evaluate
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Next(Guid quizId, Guid questionId, List<Guid> answers, Guid? nextQuestionId)
        {
            bool evaluateAtTheEnd = HttpContext.Session.GetSessionContext().EvaluationAtEnd;
            if (evaluateAtTheEnd)
            {
                //there is a next question
                if (nextQuestionId.HasValue)
                {
                    return RedirectToAction("Play", new { quizId = quizId, questionId = nextQuestionId });
                }
                //question was last
                return RedirectToAction("Completed", new { quizId = quizId });
            }

            //always validate
            _service.Validation(ModelState, questionId, answers);
            if (ModelState.IsValid)
            {
                if (nextQuestionId.HasValue)
                {
                    return RedirectToAction("Play", new { quizId = quizId, questionId = nextQuestionId });
                }
                return RedirectToAction("Completed", new { quizId = quizId });
            }
            return View("Play", _service.GetQuestion(quizId, questionId));
        }
    }
}