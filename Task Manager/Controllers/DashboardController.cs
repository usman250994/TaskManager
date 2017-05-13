using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        public ActionResult Index()
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