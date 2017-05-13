using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult CreateTask()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult ViewTask()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
    }
}
