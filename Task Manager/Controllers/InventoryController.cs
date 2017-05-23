using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult CreateInventory()
        {
            var roles_Id = Session["role_id"].ToString();
            Session["task_id"] = null;
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
        public ActionResult ViewInventory()
        {
            var roles_Id = Session["role_id"].ToString();
            Session["task_id"] = null;
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