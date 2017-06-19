using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class UserProfileApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpPost]
        public string updateProfile(userProfile req)
        {
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
            var createdUser = db.user.Find(Convert.ToInt32(id));
            if (createdUser.Password != req.oldPass)
            {
                return "Old password Incorrect";
            }
            if (req.rePass != req.newPass)
            {
                return "New Password Does't Match";
            }
            createdUser.Password = req.newPass;
            createdUser.Address = req.address;
            createdUser.MobileNo = req.number;
            createdUser.Email = req.email;

            if (db.SaveChanges() > 0)
            {
                return "Profile Updated";
            }
            else
            {
                return "Updation Failed";
            }
        }
        [HttpGet]
        public getprofile getProfile()
        {
            getprofile prof = new getprofile();
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
            var createdUser = db.user.Find(Convert.ToInt32(id));
            prof.username = createdUser.user_Name;
            prof.address = createdUser.Address;
            prof.number = createdUser.MobileNo.Substring(3,10);
            prof.email = createdUser.Email;
            return prof;
        }
    }
}