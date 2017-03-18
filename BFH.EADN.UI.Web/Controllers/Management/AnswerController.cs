using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    /// <summary>
    /// Handles CRUD operations for the answer area
    /// </summary>
    public class AnswerController : Controller
    {
        private AnswerService _service = new AnswerService();

        /// <summary>
        /// Creates a view to show all answers as a list
        /// </summary>
        /// <returns>answer list view</returns>
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        /// <summary>
        /// Creates "Details" view
        /// </summary>
        /// <param name="id">answer id</param>
        /// <param name="questionId">question id</param>
        /// <returns>answer "Details" view</returns>
        public ActionResult Details(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        /// <summary>
        /// Creates "Create" view 
        /// </summary>
        /// <param name="questionId">question id</param>
        /// <returns>answer "Create" view</returns>
        public ActionResult Create(Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View();
        }

        /// <summary>
        /// Creates new answer.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="answer">Answer model with the data</param>
        /// <returns>a RedirectToRouteResult("Edit", "Question"), or if model state is invalid the "Create" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Answer answer)
        {
            _service.Validation(ModelState, answer);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(answer);
                    return RedirectToAction("Edit", "Question", new { id = answer.QuestionId });
                }
                catch(Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }
            return View(answer);
        }

        /// <summary>
        /// Creates "Edit" view 
        /// </summary>
        /// <param name="id">id of an answer</param>
        /// <param name="questionId">question id</param>
        /// <returns>question "Edit" view</returns>
        public ActionResult Edit(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        /// <summary>
        /// Edits a question.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a answer</param>
        /// <param name="Answer">Answer model with the data</param>
        /// <returns>a RedirectToRouteResult("Details"), or if model state is invalid the "Edit" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Answer answer)
        {
            _service.Validation(ModelState, answer);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, answer);
                    return RedirectToAction("Edit", "Question", new { id = answer.QuestionId });
                }
                catch(Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }
            return View(answer);
        }

        /// <summary>
        /// Creates "Delete" view 
        /// </summary>
        /// <param name="id">id of an answer</param>
        /// <param name="questionId">question id</param>
        /// <returns>answer "Delete" view</returns>
        public ActionResult Delete(Guid id, Guid questionId)
        {
            ViewBag.QuestionId = questionId;
            return View(_service.Get(id));
        }

        /// <summary>
        /// Deletes an answer
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of an answer</param>
        /// <param name="answer">Answer model with the data</param>
        /// <returns>a RedirectToRouteResult("Edit", "Question")</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Answer answer)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Edit", "Question", new { id = answer.QuestionId });
            }
            catch
            {
                //here should be loggin
                throw;
            }
        }
    }
}
