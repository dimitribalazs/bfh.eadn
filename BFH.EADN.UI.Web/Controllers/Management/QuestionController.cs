using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    /// <summary>
    /// Handles CRUD operations for the question area
    /// </summary>
    public class QuestionController : Controller
    {
        private QuestionService _service = new QuestionService();

        /// <summary>
        /// Creates a view to show all questions as a list
        /// </summary>
        /// <returns>question list view</returns>
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        /// <summary>
        /// Creates "Details" view
        /// </summary>
        /// <param name="id">question id</param>
        /// <returns>question "Details" view</returns>
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        /// <summary>
        /// Creates "Create" view 
        /// </summary>
        /// <returns>question "Create" view</returns>
        public ActionResult Create()
        {
            return View(_service.Get());
        }

        /// <summary>
        /// Creates new question.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="question">Question model with the data</param>
        /// <returns>a RedirectToRouteResult("Index"), or if model state is invalid the "Create" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            _service.Validation(ModelState, question);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(question);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }

            Question dataFromDb = _service.Get();
            dataFromDb.IsMultipleChoice = question.IsMultipleChoice;
            dataFromDb.Hint = question.Hint;
            dataFromDb.Text = question.Text;
            return View(dataFromDb);
        }

        /// <summary>
        /// Creates "Edit" view 
        /// </summary>
        /// <param name="id">id of a question</param>
        /// <returns>question "Edit" view</returns>
        public ActionResult Edit(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        /// <summary>
        /// Edits a question.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a question</param>
        /// <param name="question">Question model with the data</param>
        /// <returns>a RedirectToRouteResult("Details"), or if model state is invalid the "Edit" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Question question)
        {
            _service.Validation(ModelState, question);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, question);
                    return RedirectToAction("Details", new { id = id });
                }
                catch(Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }

            Question dataFromDb = _service.Get(id);
            question.Answers = dataFromDb.Answers;
            question.Topics = dataFromDb.Topics;
            return View(question);
        }

        /// <summary>
        /// Creates "Delete" view 
        /// </summary>
        /// <param name="id">id of a question</param>
        /// <returns>question "Delete" view</returns>
        public ActionResult Delete(Guid id)
        {
            Question question = _service.Get(id);
            return View(question);
        }

        /// <summary>
        /// Deletes a question
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a question</param>
        /// <param name="question">Question model with the data</param>
        /// <returns>a RedirectToRouteResult("Index")</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Question question)
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

        public ActionResult WithoutTopic()
        {
            return View(_service.GetQuestionsWithoutTopics());
        }
    }
}
