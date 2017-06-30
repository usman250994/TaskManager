﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var roles_Id = Session["role_id"].ToString();
            ViewData["id"] = roles_Id;
            return View();
        }
    }
}