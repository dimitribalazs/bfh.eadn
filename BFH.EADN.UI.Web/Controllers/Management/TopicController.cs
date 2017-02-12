using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;
using System;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class TopicController : Controller
    {
        private TopicService _service = new TopicService();
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Topic/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topic/Create
        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            try
            {
                _service.Create(topic);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(topic);
            }
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(Guid id)
        {
            Topic topic = _service.Get(id);
            return View(topic);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Topic topic)
        {
            try
            {
                _service.Edit(id, topic);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View(topic);
            }
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_service.Get(id));
        }

        // POST: Topic/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Topic topic)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(topic);
            }
        }
    }
}
