  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Manager.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CreateCustomer()
        {
            return View();
        }
        public ActionResult ViewCustomer()
        {
            return View();
        }
    }
}