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
           
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            var roles_Id = Session["role_id"].ToString();
            ViewData["id"] = roles_Id;
            var userid = Session["UserId"];
            var taskid = Session["task_id"];
            if (userid != null && taskid != null)
            {
                ViewData["id"] = roles_Id;
               
                return View();
            }
            else
            {
                taskid = null;
                return RedirectToAction("Index", "Home");
            }

        }
    }
}