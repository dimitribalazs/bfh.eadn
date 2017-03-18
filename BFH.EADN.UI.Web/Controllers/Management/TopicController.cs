using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;
using System;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    /// <summary>
    /// Handles CRUD operations for the topic area
    /// </summary>
    [Authorize(Roles = Common.Constants.AdminRoleName)]
    public class TopicController : Controller
    {
        private TopicService _service = new TopicService();

        /// <summary>
        /// Creates a view to show all topics as a list
        /// </summary>
        /// <returns>topics list view</returns>
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
        /// <returns>topic "Create" view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates new topic.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="topic">topic model with the data</param>
        /// <returns>a RedirectToRouteResult("Index"), or if model state is invalid the "Create" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(topic);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //here should be loggin
                    throw;
                }
            }
            return View(topic);
        }

        /// <summary>
        /// Creates "Edit" view 
        /// </summary>
        /// <param name="id">id of a topic</param>
        /// <returns>topic "Edit" view</returns>
        public ActionResult Edit(Guid id)
        {
            Topic topic = _service.Get(id);
            return View(topic);
        }

        /// <summary>
        /// Edits a topic.
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a topic</param>
        /// <param name="topic">topic model with the data</param>
        /// <returns>a RedirectToRouteResult("Details"), or if model state is invalid the "Edit" view again</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Topic topic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(id, topic);
                    return RedirectToAction("Details", new { id = id });
                }
                catch
                {
                    //here should be loggin
                    throw;
                }
            }
            return View(topic);
        }

        /// <summary>
        /// Creates "Delete" view 
        /// </summary>
        /// <param name="id">id of a topic</param>
        /// <returns>topic "Delete" view</returns>
        public ActionResult Delete(Guid id)
        {
            Topic topic = _service.Get(id);
            return View(topic);
        }


        /// <summary>
        /// Deletes a topic
        /// Throws an exception if error happens
        /// </summary>
        /// <param name="id">id of a tppic</param>
        /// <param name="topic">topicmodel with the data</param>
        /// <returns>a RedirectToRouteResult("Index")</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Topic topic)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                //here should be loggin
                throw;
            }
        }
    }
}
