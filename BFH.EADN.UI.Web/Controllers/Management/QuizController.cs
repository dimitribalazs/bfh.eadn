using BFH.EADN.UI.Web.Models.Management;
using System;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    /// <summary>
    /// Handles CRUD operations for the quiz area
    /// </summary>
    [Authorize(Roles = Common.Constants.AdminRoleName)]
    public class QuizController : Controller
    {
        private Services.QuizService _service = new Services.QuizService();

        /// <summary>
        /// Creates a view to show all quizzes as a list
        /// </summary>
        /// <returns>quiz list view</returns>
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        /// <summary>
        /// Creates "Details" view
        /// </summary>
        /// <param name="id">quiz id</param>
        /// <returns>quiz "Details" view</returns>
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        /// <summary>
        /// Creates "Create" view 
        /// </summary>
        /// <returns>quiz "Create" view</returns>
        public ActionResult Create()
        {
            return View(_service.Get());
        }

        /// <summary>
        /// Creates new quiz.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="quiz">Quiz model with the data</param>
        /// <returns>a RedirectToRouteResult("Index"), or if model state is invalid the "Create" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            _service.Validation(ModelState, quiz);
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
            _service.AddQuestionsToQuiz(quiz);
            return View(quiz);
        }

        /// <summary>
        /// Creates "Edit" view 
        /// </summary>
        /// <param name="id">id of a quiz</param>
        /// <returns>quiz "Edit" view</returns>
        public ActionResult Edit(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        /// <summary>
        /// Edits a quiz.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a quiz</param>
        /// <param name="quiz">Quiz model with the data</param>
        /// <returns>a RedirectToRouteResult("Details"), or if model state is invalid the "Edit" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Quiz quiz)
        {
            _service.Validation(ModelState, quiz);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, quiz);
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }
            _service.AddQuestionsToQuiz(quiz);
            return View(quiz);
        }

        /// <summary>
        /// Creates "Delete" view 
        /// </summary>
        /// <param name="id">id of a quiz</param>
        /// <returns>quiz "Delete" view</returns>
        public ActionResult Delete(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        /// <summary>
        /// Deletes a quiz
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a quiz</param>
        /// <param name="quiz">Quiz model with the data</param>
        /// <returns>a RedirectToRouteResult("Index")</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Quiz quiz)
        {
            try
            { 
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //here should be loggin
                throw;
            }
        }
    }
}
