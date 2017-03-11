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

        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Edit(Guid id)
        {
            Topic topic = _service.Get(id);
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Delete(Guid id)
        {
            Topic topic = _service.Get(id);
            return View(topic);
        }

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
                return View(topic);
            }
        }
    }
}
