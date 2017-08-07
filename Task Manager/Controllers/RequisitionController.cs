using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class RequisitionController : Controller
    {
        // GET: Requisition
        public ActionResult CreateRequisition()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            Session["task_id"] = null;
            string roles_Id = Session["role_id"].ToString();
            if (Session["UserId"] != null && (roles_Id == "1" || roles_Id == "2" || roles_Id == "3"))
            {
                ViewData["id"] = roles_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult ViewRequisition()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            Session["task_id"] = null;
            if (Session["UserId"] != null)
            {
                string roles_Id = Session["role_id"].ToString();
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