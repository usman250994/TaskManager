using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class messageController : Controller
    {

        // GET: message
        public ActionResult ViewMessage()
        {
            var userid = Session["UserId"].ToString();
            var taskid = Session["task_id"].ToString();
            if (userid != null && taskid != null)
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