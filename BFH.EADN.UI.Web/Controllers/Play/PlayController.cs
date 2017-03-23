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

        //keys used for cookies
        private static readonly string _quizState = "QuizState";
        private static readonly string _questionAnswerStateId = "QuestionAnswerStateId";

        private static readonly string _url = "Url";
        private static readonly string _evaluationAtEnd = "EvaluationAtEnd";
        private static readonly string _questionsInCurrentQuiz = "questionsInCurrentQuizId";

        /// <summary>
        /// Lists all quizzes grouped by topics
        /// </summary>
        /// <returns>play index view</returns>
        public ActionResult Index()
        {
            //get overviews
            List<Overview> overview = _service.GetOverview();

            //get quizState cookie (already existing or new one)
            HttpCookie cookie = GetCookie(_quizState);

            //and check if there has already been a played quiz
            if (overview.Count > 0 && string.IsNullOrEmpty(cookie.Values[_url]) == false)
            {
                string url = HttpUtility.UrlDecode(cookie.Values[_url]);
                if (_service.UrlIsStillValid(url))
                {
                    //set already played quiz url. Displays button on page
                    overview.First().ContinueQuizUrl = url;
                }
            }

            //get the evaluation type of the already played quiz
            //and set it to the session
            bool evalAtEnd;
            if (bool.TryParse(cookie.Values[_evaluationAtEnd], out evalAtEnd))
            {
                HttpContext.Session.GetSessionContext().EvaluationAtEnd = evalAtEnd;
            }

            //set the cookie to the response
            HttpContext.Response.Cookies.Set(cookie);
            return View(overview);
        }

        /// <summary>
        /// Gets called if quiz gets startet new
        /// </summary>
        /// <param name="quizId">quiz id</param>
        /// <returns>validation type view</returns>
        public ActionResult ValidationType(Guid quizId)
        {
            //get questionAnswerStateId cookie
            HttpCookie cookie = GetCookie(_questionAnswerStateId);

            //get quizState cookie, we need this cookie to save all the question ids
            HttpCookie quizStateCookie = GetCookie(_quizState);

            //check if there is already a saved 
            if (string.IsNullOrEmpty(cookie.Value))
            {
                //create new questionAnswerStateId
                cookie.Value = Guid.NewGuid().ToString();
            }
            
            //clear old state
            Guid questionAnswerStateId = Guid.Parse(cookie.Value);
            _service.DeleteQuestionAnswerState(questionAnswerStateId);

            ContractTypes.Quiz quiz = _service.GetQuiz(HttpContext, quizId);

            //save questions in quiz
            quizStateCookie[_questionsInCurrentQuiz] = string.Join(",", quiz.Questions.Select(q => q.Id));
            
            //update response
            HttpContext.Response.Cookies.Remove(_questionAnswerStateId);
            HttpContext.Response.Cookies.Set(cookie);

            HttpContext.Response.Cookies.Remove(_quizState);
            HttpContext.Response.Cookies.Set(quizStateCookie);


            ValidationType type = new ValidationType();
            type.QuizId = quizId;
            Question question = _service.GetFirstQuestion(quiz);
            type.QuestionId = question.QuestionId;
            return View(type);
        }

        /// <summary>
        /// Saves the validation type to session and cookie
        /// </summary>
        /// <param name="type">validation type</param>
        /// <returns>RedirectToActionResult("Play")</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationType(ValidationType type)
        {
            //clear session data
            SessionContext sc = HttpContext.Session.GetSessionContext();
            sc.AnswersForEndEvaluations.Clear();

            //set evaluation type
            sc.EvaluationAtEnd = type.EvaluationAtEnd;
            HttpCookie cookie = GetCookie(_quizState);
            cookie.Values[_evaluationAtEnd] = type.EvaluationAtEnd.ToString();

            //update response
            HttpContext.Response.Cookies.Remove(_quizState);
            HttpContext.Response.Cookies.Set(cookie);

            return RedirectToAction("Play", new { quizId = type.QuizId, questionId = type.QuestionId });
        }

        /// <summary>
        /// Play the quiz 
        /// </summary>
        /// <param name="quizId">quiz id</param>
        /// <param name="questionId">question id, can be null</param>
        /// <returns>"Play" view</returns>
        public ActionResult Play(Guid quizId, Guid? questionId)
        {
            //update cookie with new url
            HttpCookie cookie = GetCookie(_quizState);
            cookie.Values[_url] = HttpUtility.UrlEncode(HttpContext.Request.Url.AbsoluteUri);

            string questionIds = string.IsNullOrEmpty(cookie.Values[_questionsInCurrentQuiz]) == false
                                ? cookie.Values[_questionsInCurrentQuiz]
                                : null;

            HttpCookie questionAnswerStateCookie = GetCookie(_questionAnswerStateId);
            ContractTypes.Quiz quiz;
            Guid questionAnswerStateId;
            if (Guid.TryParse(questionAnswerStateCookie.Value, out questionAnswerStateId))
            {
                
                quiz = _service.GetQuiz(HttpContext, quizId, questionAnswerStateId, questionIds);
            }
            else
            {
                quiz = _service.GetQuiz(HttpContext, quizId, null, questionIds);
            }
            


            HttpContext.Response.Cookies.Remove(_quizState);
            HttpContext.Response.Cookies.Set(cookie);
            
            Question question = _service.GetQuestion(quiz, questionId.Value);
            return View(question);
        }

        /// <summary>
        /// Complete the quiz.
        /// If completed and evaluation at the end was selected, there will be an evaluation overview
        /// </summary>
        /// <param name="quizId">quiz id</param>
        /// <returns>"Completed" view or "CompletedWithData" view</returns>
        public ActionResult Completed(Guid quizId)
        {
            bool evaluateAtTheEnd = HttpContext.Session.GetSessionContext().EvaluationAtEnd;
            if (evaluateAtTheEnd)
            {
                //evaluate
                HttpCookie cookie = GetCookie(_questionAnswerStateId);
                Guid questionAnswerStateId = Guid.Parse(cookie.Value);
                List<Complete> complete = _service.EvaluateAnswers(questionAnswerStateId);
                return View("CompletedWithData", complete);
            }
            return View();
        }

        /// <summary>
        /// Get next answer or complete page
        /// </summary>
        /// <param name="quizId">quiz id</param>
        /// <param name="questionId">question id</param>
        /// <param name="answers">list of ids</param>
        /// <param name="nextQuestionId">nextQuestionId, can be null</param>
        /// <returns>
        /// if evaluation at the end RedirectToActionResult (Play/Completed),
        /// if answers are validated after each question then, if answer is correct then RedirectToActionResult (Play/Completed),
        /// else view "Play"
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Next(Guid quizId, Guid questionId, List<Guid> answers, Guid? nextQuestionId)
        {
            SessionContext sessionContext = HttpContext.Session.GetSessionContext();
            bool evaluateAtTheEnd = sessionContext.EvaluationAtEnd;
            if (evaluateAtTheEnd)
            {
                //cookie must already exists to continue from here
                HttpCookie cookie = GetCookie(_questionAnswerStateId);
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
        /// <param name="key">cookie name</param>
        /// <returns>httpcookie</returns>
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