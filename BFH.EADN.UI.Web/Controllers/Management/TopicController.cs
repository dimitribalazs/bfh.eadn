using BFH.EADN.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class TopicController : Controller
    {
        static List<Topic> topics = new List<Topic>
            {
                new Topic { Id = Guid.NewGuid(), Description = "Description 1", Name = "Name 1" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 2", Name = "Name 2" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 3", Name = "Name 3" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 4", Name = "Name 4" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 5", Name = "Name 5" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 6", Name = "Name 6" },
                new Topic { Id = Guid.NewGuid(), Description = "Description 7", Name = "Name 7" },
            };
        // GET: Topic
        public ActionResult Index()
        {
            
            return View(topics);
        }

        // GET: Topic/Details/5
        public ActionResult Details(Guid id)
        {
            return View(topics.Find(t => t.Id == id));
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
                // TODO: Add insert logic here
                if(topic.Id == default(Guid))
                {
                    topic.Id = Guid.NewGuid();
                }
                topics.Add(topic);
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
            return View(topics.Find(t => t.Id == id));
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Topic topic)
        {
            try
            {
                // TODO: Add update logic here
                topics.Remove(topics.Find(t => t.Id == id));
                topics.Add(topic);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(topic);
            }
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(topics.Find(t => t.Id == id));
        }

        // POST: Topic/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Topic topic)
        {
            try
            {
                // TODO: Add delete logic here
                topics.Remove(topics.Find(t => t.Id == id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View(topic);
            }
        }
    }
}
