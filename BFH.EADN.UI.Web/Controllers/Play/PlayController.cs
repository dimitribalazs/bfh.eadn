using ContractTypes = BFH.EADN.Common.Types.Contracts;
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
            ContractTypes.Quiz quiz;
            //check if there is already a quiz in the session
            if (HttpContext.Session.GetSessionContext().CurrentQuiz == null)
            {
                HttpContext.Session.GetSessionContext().CurrentQuiz = _service.GetContractQuiz(quizId);
            }
            quiz = HttpContext.Session.GetSessionContext().CurrentQuiz;

            ValidationType type = new ValidationType();
            type.QuizId = quizId;
            type.QuestionId = _service.GetFirstQuestion(quiz).QuestionId;
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
            ContractTypes.Quiz quiz;
            //check if there is already a quiz in the session
            if(HttpContext.Session.GetSessionContext().CurrentQuiz == null)
            { 
                HttpContext.Session.GetSessionContext().CurrentQuiz = _service.GetContractQuiz(quizId);
            }

            quiz = HttpContext.Session.GetSessionContext().CurrentQuiz;

            //update cookie with new url
            HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            cookie.Values["Url"] = HttpUtility.UrlEncode(HttpContext.Request.Url.AbsoluteUri);
            HttpContext.Response.Cookies.Remove("QuizState");
            HttpContext.Response.Cookies.Set(cookie);
            
            return View(_service.GetQuestion(quiz, questionId.Value));
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

            ContractTypes.Quiz quiz;
            //check if there is already a quiz in the session
            if (HttpContext.Session.GetSessionContext().CurrentQuiz == null)
            {
                HttpContext.Session.GetSessionContext().CurrentQuiz = _service.GetContractQuiz(quizId);
            }
            quiz = HttpContext.Session.GetSessionContext().CurrentQuiz;

            return View("Play", _service.GetQuestion(quiz, questionId));
        }
    }
}