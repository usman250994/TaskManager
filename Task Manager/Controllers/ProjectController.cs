﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult CreateProject()
        {
            return View();
        }
        public ActionResult ViewProject()
        {
            return View();
        }
    }
}