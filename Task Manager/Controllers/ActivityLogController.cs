using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class ActivityLogController : Controller
    {
        // GET: ActivityLog
        public ActionResult Index()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            if ( Session["role_id"].ToString() == "1")
            {

                ViewData["id"] = 1;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
    }
}