using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.UI.Web.Models.Play;
using BFH.EADN.UI.Web.Services;
using BFH.EADN.UI.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace BFH.EADN.UI.Web.Controllers.Play
{
    public class PlayController : Controller
    {
        private PlayService _service = new PlayService();
        private static readonly string _quizState = "QuizState";
        private static readonly string _quizStateId = "QuizStateId";
        private static readonly string _url = "Url";
        private static readonly string _evaluationAtEnd = "EvaluationAtEnd";
        [HttpGet]
        public ActionResult Index()
        {
            List<Overview> overview = _service.GetOverview();
            //HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            HttpCookie cookie = GetCookie(_quizState);
            if(cookie.Values[_url] != null)
            {
                overview.First().ContinueQuizUrl = HttpUtility.UrlDecode(cookie.Values[_url]);
            }

            bool evalAtEnd;
            if (bool.TryParse(cookie.Values["EvaluationAtEnd"], out evalAtEnd))
            {
                HttpContext.Session.GetSessionContext().EvaluationAtEnd = evalAtEnd;
            }
            
            HttpContext.Response.Cookies.Set(cookie);
            return View(overview);
        }

        /// <summary>
        /// Gets called if quiz gets startet new
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ValidationType(Guid quizId)
        {
            HttpCookie cookie = GetCookie(_quizStateId);
            if (string.IsNullOrEmpty(cookie.Value))
            {
                cookie.Value = Guid.NewGuid().ToString();
            }
            
            HttpContext.Response.Cookies.Remove(_quizState);
            HttpContext.Response.Cookies.Set(cookie);

            //clear old state
            Guid questionAnswerStateId = Guid.Parse(cookie.Value);
            _service.DeleteQuestionAnswerState(questionAnswerStateId);

            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

            ValidationType type = new ValidationType();
            type.QuizId = quizId;
            Question question = _service.GetFirstQuestion(quiz);
            type.QuestionId = question.QuestionId;
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationType(ValidationType type)
        {
            //clear session stuff
            SessionContext sc = HttpContext.Session.GetSessionContext();
            sc.AnswersForEndEvaluations.Clear();

            sc.EvaluationAtEnd = type.EvaluationAtEnd;

            HttpCookie cookie = GetCookie(_quizState);
            cookie.Values[_evaluationAtEnd] = type.EvaluationAtEnd.ToString();
            return RedirectToAction("Play", new { quizId = type.QuizId, questionId = type.QuestionId });
        }

        [HttpGet]
        public ActionResult Play(Guid quizId, Guid? questionId)
        {
            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

            //update cookie with new url
            HttpCookie cookie = GetCookie(_quizState);
            cookie.Values[_url] = HttpUtility.UrlEncode(HttpContext.Request.Url.AbsoluteUri);
            HttpContext.Response.Cookies.Remove(_quizState);
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
                HttpCookie cookie = GetCookie(_quizStateId);
                Guid questionAnswerStateId = Guid.Parse(cookie.Value);
                List<Complete> complete = _service.EvaluateAnswers(questionAnswerStateId);
                return View("CompletedWithData", complete);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Next(Guid quizId, Guid questionId, List<Guid> answers, Guid? nextQuestionId)
        {
            SessionContext sessionContext = HttpContext.Session.GetSessionContext();
            bool evaluateAtTheEnd = sessionContext.EvaluationAtEnd;
            if (evaluateAtTheEnd)
            {
                //cookie must already exists to continue from here
                HttpCookie cookie = GetCookie(_quizStateId);
                _service.SaveQuestionAnswerState(Guid.Parse(cookie.Value), questionId, answers);

                Dictionary<Guid, List<Guid>> questionAnswers = sessionContext.AnswersForEndEvaluations;
                if (questionAnswers.ContainsKey(questionId))
                {
                    questionAnswers[questionId] = answers;
                }
                else
                {
                    questionAnswers.Add(questionId, answers);
                }

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

            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

            return View("Play", _service.GetQuestion(quiz, questionId));
        }

        /// <summary>
        /// Return cookie by a key. If it doesnt exists, it will be created
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private HttpCookie GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get(key);
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            return cookie;
        }
    }
}