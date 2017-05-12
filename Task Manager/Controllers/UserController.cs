using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class UserController : Controller
    {
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult ViewUser()
        {
            return View();
        }

    }
}
