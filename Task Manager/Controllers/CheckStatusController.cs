using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class CheckStatusController : Controller
    {
        // GET: CheckStatus

        public ActionResult Index()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}