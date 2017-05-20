using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class UserApiController : ApiController
    {
        TaskContext db = new TaskContext();

        [Route("/api/UserApi/"), HttpPost]
        public String CreateUser(userRequest Emp)
        {

            var user = db.user.Find(Emp.id);
            //user already exisits
            if (user != null)
            {
                var userRole = db.roles.Find(Emp.Roles_id);
                user.user_Name = Emp.user_Name;
                user.Password = Emp.Password;
                user.MobileNo = Emp.MobileNo;
                user.Email = Emp.Email;
                user.Address = Emp.Address;
                user.Roles_id = userRole;
                user.Enable = true;
                user.Createdon = DateTime.Now;
                //db.Entry(user).CurrentValues.SetValues(Emp);
                if (db.SaveChanges() > 0)
                {
                    return "User updated Succesfully";
                }
                else
                {
                    return "User Not updated";
                }
            }
            //Add user here
            else
            {
                var sessionId = HttpContext.Current.Session;
                string id = sessionId["UserID"].ToString();
                var createdUser = db.user.Find(Convert.ToInt32(id));
                var userRole = db.roles.Find(Emp.Roles_id);
                Users adduser = new Users();
                adduser.user_Name = Emp.user_Name;
                adduser.Password = Emp.Password;
                adduser.MobileNo = Emp.MobileNo;
                adduser.Address = Emp.Address;
                adduser.Email = Emp.Email;
                adduser.Enable = true;
                adduser.Createdon = DateTime.Now;
                adduser.Roles_id = userRole;
                adduser.Created_By = createdUser;

                db.user.Add(adduser);
                if (db.SaveChanges() > 0)
                {
                    return "User Added Successfully";
                }
                else
                    return "Unable to Add User";
            }
        }


        [Route("/api/UserApi/"), HttpPost]
        public List<Roles> Dropdownuser(int id)
        {
            List<Roles> role = new List<Roles>();
            role = db.roles.Where(p => p.enable == true).ToList();
            return role;
        }


        //Delete user function
        [Route("/api/UserApi/"), HttpDelete]
        public String delete(int id)
        {
            var user = db.user.Where(p => p.id == id).FirstOrDefault().Enable = false;

            if (db.SaveChanges() == 1)
            {
                return "You Want To Delete This User ?";
            }
            else
            {
                return "User Not Deleted";
            }
        }

        [Route("/api/UserApi/"), HttpPut]
        public int set(int id)
        {
            var session = HttpContext.Current.Session;
            session["user"] = id;
            return 0;
        }

        [Route("/api/UserApi/"), HttpPut]
        public string get()
        {
            var session = HttpContext.Current.Session;
            if (session["user"] != null)
            {
                string str = session["user"].ToString();
                session["user"] = null;
                return str;
            }
            else
            {
                return "0";
            }
        }


        //To Get All The User List (To Show in Grid View Of User)
        [Route("/api/UserApi/"), HttpGet]
        public List<Users> Userall()
        {
            List<Users> list = new List<Users>();

            var session = HttpContext.Current.Session;

            if (session["UserID"].ToString() == "5")
            {
                list = db.user.Where(p => p.Enable == true).ToList();
            }
            else
            {
                var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));

                list = db.user.Where(p => p.Enable == true && p.Created_By.id == createdBy.id).ToList();
            }


            return list;
        }


        //to get a single user for filling form
        [Route("/api/UserApi/"), HttpGet]
        public Users Userall(int id)
        {
            var user = db.user.Where(p => p.id == id).FirstOrDefault();
            return user;
        }

    }
}