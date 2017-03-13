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
            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

            ValidationType type = new ValidationType();
            type.QuizId = quizId;
            type.QuestionId = _service.GetFirstQuestion(quiz).QuestionId;
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

            HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizState");
            cookie.Values["EvaluationAtEnd"] = type.EvaluationAtEnd.ToString();
            return RedirectToAction("Play", new { quizId = type.QuizId, questionId = type.QuestionId });
        }

        [HttpGet]
        public ActionResult Play(Guid quizId, Guid? questionId)
        {
            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

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
                HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizStateId");
                Dictionary<Guid, List<Guid>> questionAnswers = new Dictionary<Guid, List<Guid>>();
                foreach(string cookieEntry in cookie.Values.AllKeys)
                {
                    List<Guid> value = Serializer.DeserializeFromBase64String<List<Guid>>(cookie.Values[cookieEntry]);
                    questionAnswers.Add(Guid.Parse(cookieEntry), value);
                }
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
                Dictionary<Guid, List<Guid>> questionAnswers = HttpContext.Session.GetSessionContext().AnswersForEndEvaluations;
                questionAnswers.Add(questionId, answers);

                HttpCookie cookie = HttpContext.Request.Cookies.Get("QuizStateId");
                if(cookie == null)
                {
                    cookie = new HttpCookie("QuizStateId");
                }
                if(string.IsNullOrEmpty(cookie.Value))
                {
                    cookie.Value = Guid.NewGuid().ToString();
                }
                //if(answers == null)
                //{
                //    answers = new List<Guid>();
                //}
                //string value = Serializer.SerializeToBase64String(answers);
                //cookie.Values.Add(questionId.ToString(), value);
                //foreach(KeyValuePair<Guid, List<Guid>> data in questionAnswers)
                //{
                //    string value = Serializer.SerializeToBase64String(data.Value);
                //    cookie.Values.Add(data.Key.ToString(), value);
                //}
                //cookie.Value = HttpUtility.UrlEncode(Serializer.SerializeToBase64String(questionAnswers));
                HttpContext.Response.Cookies.Remove("QuizStateId");
                HttpContext.Response.Cookies.Set(cookie);
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
    }
}