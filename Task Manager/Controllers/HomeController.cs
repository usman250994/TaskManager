using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Task_Manager.Models;


namespace Task_Manager.Controllers
{
    public class HomeController : Controller
    {
        TaskContext DB = new TaskContext();

        public ActionResult Index()
        {
            return View();
        }
       
        //public IEnumerable<Users> GetAllUser()
        //{
        //    var Users = DB.user.ToList();
        //    return Users;
        //}

        //public IHttpActionResult GetUser(int id)
        //{
        //    var uu = DB.user.FirstOrDefault(p => p.id == id);
        //    if (uu == null)
        //    {
        //        return null ;
        //    }
        //    return null;
        //}
    }
}
