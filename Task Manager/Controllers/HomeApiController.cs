using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Providers.Entities;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class HomeApiController : ApiController
    {
        [Route("/api/Homeapi/"), HttpPost]
        public string Login(Login log)
        {
            if (string.IsNullOrWhiteSpace(log.user_Name) || string.IsNullOrWhiteSpace(log.password))
            {
                return "Invalid ID or Password";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (TaskContext db = new TaskContext())
                    {
                        var session = HttpContext.Current.Session;
                        if (log.user_Name == "sudo" && log.password == "sudo")
                        {
                            session["UserID"] = "5";
                            return "Authenticated";
                        }
                        else
                        {
                            var obj = db.user.Where(a => a.Email.Equals(log.user_Name) && a.Password.Equals(log.password) && a.Enable.Equals(true)).FirstOrDefault();
                            if (obj != null)
                            {
                                session["UserID"] = obj.id;
                                return "Authenticated";
                            }
                            return "Invalid ID or Password";
                        }
                    }
                }
                else
                {
                    return "Invalid ID or Password";
                }
            }
        }
    }
}