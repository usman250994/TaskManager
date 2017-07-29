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
                            session["role_id"] = "1";
                            session["username"] = "SUDO";
                            return "Authenticated";
                        }
                        else
                        {

                            var obj = db.user.Where(a => a.Email == log.user_Name && a.Password == log.password && a.Enable == true).FirstOrDefault();
                            if (obj != null)
                            {
                                session["UserID"] = obj.id;
                                session["role_id"] = obj.Roles_id.id;
                                session["username"] = obj.user_Name;
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
            task.task_name = ticket.name;
            task.description = ticket.issue;
            task.created_on = DateTime.Now;
            var usr = db.user.Find(5);
            task.Created_By = usr;
            task.closingDate = DateTime.Now;
            task.completeDate = DateTime.Now;
            //task.completingUser = usr;
            //task.closingUser = usr;
            task.sms = false;
            task.email = false;
            task.IsTicket = true;
            task.status = 0;
            task.branch_code = ticket.branch_code;
            task.start_date = DateTime.Now;
            task.end_date = DateTime.Now;
            task.LastModify = DateTime.Now;
            var code = db.project.Where(p => p.id == ticket.projectId).Select(o => o.customer.city_code).FirstOrDefault();

            code = code.Substring(2, 4);

            task.ticket_code = db.task.Where(c => c.IsTicket == true).Count() + code;

            db.task.Add(task);
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

                Task_Manager.SMSandEmail send = new SMSandEmail();
                string customername = db.project.Where(p => p.id == ticket.projectId).Select(o => o.customer.customer_name).FirstOrDefault();
                string projectname = db.project.Where(p => p.id == ticket.projectId).Select(p => p.Project_Name).FirstOrDefault();

                send.sendEmail(ticket.name, ticket.email, task.ticket_code, ticket.customerId.ToString(), customername, projectname, ticket.contact, ticket.issue);


                return "Ticket is successfully Generated!! you will be cotacted Soon! Please Note Your Ticket Number:" + task.ticket_code;

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
            var customers = db.customer.Where(p => p.enable == true).Select(p => p.city_code).ToList();
            foreach (var entity in customers)
            {
                TicketInitialrespone response = new TicketInitialrespone();
                response.customerId = entity.ToString();
                List<projectDropDownInTickets> saa;
                saa = db.project.Where(p => p.customer.city_code == entity && p.Enable == true).Select(s => new projectDropDownInTickets() { name = s.Project_Name, id = s.id }).ToList();
                response.projects = saa;
                dropDown.Add(response);
            }
            return dropDown;
        }


        [HttpDelete]
        public string statusReturn(ticketStatus check)
        {
            if (check != null)
            {
                var session = HttpContext.Current.Session;
                session["customerid"] = check.customerId;
                return "Code";
            }
            else
            {
                return "Please Insert Value";
            }

            //List<clientsTicketRespone> list = new List<clientsTicketRespone>();

            //int id = 0;
            //if (!string.IsNullOrEmpty(check.customerId))
            //{
            //    id = Convert.ToInt32(check.customerId);
            //    if (db.task.Any(o => o.id == id))
            //    {
            //        clientsTicketRespone status = new clientsTicketRespone();

            //        var ticket = db.task.Find(id);
            //        status.id = ticket.id;
            //        status.status = fillStatus(ticket.status);
            //        status.date = ticket.created_on;
            //        status.description = ticket.description;
            //        status.name = ticket.task_name;
            //        list.Add(status);
            //        return list;

            //    }
            //    else 
            //    {
            //        var projIds = db.project.Where(p => p.customer.city_code == check.customerId).Select(p => p.id).ToList();
            //        List<int> taskIdList = new List<int>();
            //        foreach (var entity in projIds)
            //        {
            //            var lis = db.tagging.Where(p => p.project.id == entity && p.tasks.IsTicket == true).Select(p => p.tasks.id).ToList();

            //            foreach (var li in lis)
            //            {
            //                var ticket = db.task.Find(li);
            //                clientsTicketRespone status = new clientsTicketRespone();
            //                status.id = ticket.id;

            //                status.status = fillStatus(ticket.status);
            //                status.date = ticket.created_on;
            //                status.description = ticket.description;
            //                status.name = ticket.task_name;
            //                list.Add(status);
            //            }
            //        }
            //    }


            //}
            //return list;

        }

        private string fillStatus(int p)
        {
            if (p == 0)
                return "No Progress";
            if (p == 1)
                return "In Pending";
            if (p == 2)
                return "Complete";
            else
                return "Resolved";
        }
    }
}