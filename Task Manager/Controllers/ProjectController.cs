using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult CreateProject()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["task_id"] = null;
            var roles_Id = Session["role_id"].ToString();
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            if (Session["UserId"] != null && (roles_Id == "1" || roles_Id == "2"))
            {

                ViewData["id"] = roles_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult ViewProject()
        {

            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Session["task_id"] = null;
            var roles_Id = Session["role_id"].ToString();
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            if (Session["UserId"] != null && (roles_Id == "1" || roles_Id == "2" || roles_Id == "3"||roles_Id=="9"))
            {
                ViewData["id"] = roles_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult ProjectFileUpload()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            Session["task_id"] = null;
            var roles_Id = Session["role_id"].ToString();
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
    }
}