﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult CreateTicket()
        {
            string roles_Id = Session["role_id"].ToString();
            if (Session["UserId"] != null && (roles_Id == "1" || roles_Id == "2" || roles_Id == "4"))
            {

                ViewData["id"] = roles_Id;



                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult ViewTicket()
        {
            var roles_Id = Session["role_id"].ToString();
            if (Session["UserId"] != null )
            {
                ViewData["id"] = roles_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
    }
}