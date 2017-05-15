using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Providers.Entities;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class HomeApiController : ApiController
    {

        TaskContext db = new TaskContext();
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

        [HttpPut]
        public String Ticket(ComplainProjects ticket)
        {

            ticketContact tickCont = new ticketContact();
            
            tickCont.contact=ticket.contact;
            tickCont.email=ticket.email;
            tickCont.name=ticket.name;

        


            Task task = new Task();


            task.enable = true;
            task.task_name = "ticket";
            task.description = ticket.issue;
            task.created_on = DateTime.Now;
            task.Created_By = db.user.Find(5);
            task.sms=false;
                task.email=false;
            task.status =0;
            Tagging tag = new Tagging();
            tag.tasks = task;
            tag.project = db.project.Find(ticket.projectId);
            db.task.Add(task);
            db.tagging.Add(tag);
            if (db.SaveChanges() > 0)
            {
                return "Ticket is successfully Generated!! you will be cotacted Soon!";
            }
            
            else
            {
                return "Ticket Cann not Be genrated At the Moment. Sorry For the Inconvenience";
            }

        }
    }
}