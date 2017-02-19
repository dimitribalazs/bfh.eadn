﻿using BFH.EADN.UI.Web.Models.Management;
using BFH.EADN.UI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Controllers.Management
{
    public class QuizController : Controller
    {
        private QuizService _service = new QuizService();
        // GET: Quiz
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Quiz/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_service.Get(id));
        }

        // GET: Quiz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quiz/Create
        [HttpPost]
        public ActionResult Create(Quiz quiz)
        {
            try
            {
                // TODO: Add insert logic here
                _service.Create(quiz);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(quiz);
            }
        }

        // GET: Quiz/Edit/5
        public ActionResult Edit(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Quiz quiz)
        {
            try
            {
                _service.Edit(id, quiz);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View(quiz);
            }
        }

        // GET: Quiz/Delete/5
        public ActionResult Delete(Guid id)
        {
            Quiz quiz = _service.Get(id);
            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Quiz quiz)
        {
            try
            {
                // TODO: Add delete logic here
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(quiz);
            }
        }
    }
}
