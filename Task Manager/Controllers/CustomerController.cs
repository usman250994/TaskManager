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
            if (Session["UserId"] != null)
            {
              
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult ViewCustomer()
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