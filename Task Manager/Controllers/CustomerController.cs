using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;

namespace Task_Manager.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        public ActionResult CreateCustomer()
        {

            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            var roles_Id = Session["role_id"].ToString();
            Session["task_id"] = null;
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
        public ActionResult ViewCustomer()
        {
            if (Session["role_id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string userName = Session["username"].ToString();
            ViewData["userName"] = userName;
            var roles_Id = Session["role_id"].ToString();
            if (Session["UserId"] != null && (roles_Id == "1" || roles_Id == "2" || roles_Id=="4"))
            {
                Session["task_id"] = null;
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