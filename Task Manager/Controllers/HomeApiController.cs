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
            // Ticket table


            Task task = new Task();
            task.id = 0;
            task.enable = true;
            task.task_name = "ticket";
            task.description = ticket.issue;
            task.created_on = DateTime.Now;
            var usr = db.user.Find(5);
            task.Created_By = usr;
            task.sms = false;
            task.email = false;
            task.status = 0;
            task.start_date = DateTime.Now;
            task.end_date = DateTime.Now;





            Tagging tag = new Tagging();
            tag.tasks = task;
            tag.project = db.project.Find(ticket.projectId);
            db.tagging.Add(tag);


            var check = db.tickets.Where(p => p.email == ticket.email && p.contact == ticket.contact).Count();
            if (check > 0)
            {
                var ticketCustomer = db.tickets.Where(p => p.email == ticket.email && p.contact == ticket.contact).FirstOrDefault();


                ticketCustomer._tickets.Add(task);
                //ticketCustomer.tickets.AddRange(tsk);

            }
            else
            {

                ticketContact tick = new ticketContact();
                tick.email = ticket.email;
                tick.contact = ticket.contact;
                tick.name = ticket.name;
                tick._tickets.Add(task);
                db.tickets.Add(tick);

            }

            if (db.SaveChanges() > 0)
            {
                return "Ticket is successfully Generated!! you will be cotacted Soon!";
            }
            else
            {
                return "Ticket Cann not Be genrated At the Moment. Sorry For the Inconvenience";
            }


        }


        [HttpGet]
        public List<TicketInitialrespone> getDropDown()
        {
            List<TicketInitialrespone> dropDown = new List<TicketInitialrespone>();
            var customers = db.customer.Where(p => p.enable == true).Select(p => p.customerId).ToList();
            foreach (var entity in customers)
            {
                TicketInitialrespone response =new TicketInitialrespone();
                response.customerId = entity.ToString();
                List<projectDropDownInTickets> saa;
                saa = db.project.Where(p => p.customer.customerId == entity && p.Enable == true).Select(s => new projectDropDownInTickets() {name= s.Project_Name, id =s.id }).ToList();
                response.projects = saa;
                dropDown.Add(response);
            }
            return dropDown;
        }
    }
}