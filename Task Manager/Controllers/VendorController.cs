using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult CreateVendor()
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
        public ActionResult ViewVendor()
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
        public ActionResult VendorContacts()
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
    }
}