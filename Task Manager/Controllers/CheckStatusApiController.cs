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
    public class CheckStatusApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpPost]
        public ticketStatusForClient statusReturn()
        {
            ticketStatusForClient tick = new ticketStatusForClient();

           
            List<clientsTicketRespone> list = new List<clientsTicketRespone>();
            var Session = HttpContext.Current.Session;
            string customerid = Session["customerid"].ToString();

            if (!string.IsNullOrEmpty(customerid))
            {
              string  id = customerid;
                if (db.task.Any(o => o.ticket_code == id && o.enable == true))
                {
                    clientsTicketRespone status = new clientsTicketRespone();

                    var ticket = db.task.Where(o=>o.ticket_code==id).FirstOrDefault();
                    status.tid = ticket.ticket_code;
                    status.status = fillStatus(ticket.status);
                    status.date = ticket.created_on;
                    status.description = ticket.description;
                    status.name = ticket.task_name;
                    status.branch_code = ticket.branch_code;
                    status.project_name = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.Project_Name).FirstOrDefault();

                    var projId = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.id).FirstOrDefault();
                              
                    list.Add(status);
                    
                    tick.code= db.tagging.Where(c=>c.tasks.ticket_code==id).Select(p=>p.project.customer.city_code).FirstOrDefault();
                    tick.customer_name=db.tagging.Where(c=>c.tasks.ticket_code==id).Select(p=>p.project.customer.customer_name).FirstOrDefault();
                }
                else
                {
                    var projIds = db.project.Where(p => p.customer.city_code == customerid).Select(p => p.id).ToList();
                    List<int> taskIdList = new List<int>();
                    foreach (var entity in projIds)
                    {
                        var lis = db.tagging.Where(p => p.project.id == entity && p.tasks.IsTicket == true && p.tasks.enable == true).Select(p => p.tasks.id).ToList();

                        foreach (var li in lis)
                        {
                            var ticket = db.task.Find(li);
                            clientsTicketRespone status = new clientsTicketRespone();
                            status.tid = ticket.ticket_code;
                            status.status = fillStatus(ticket.status);
                            status.date = ticket.created_on;
                            status.branch_code = ticket.branch_code;
                            status.description = ticket.description;
                            status.name = ticket.task_name;                         
                            status.project_name = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.Project_Name).FirstOrDefault();
                            var projId = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.id).FirstOrDefault();
                            list.Add(status);
                        }
                    }
              
                    tick.code=customerid;
                    tick.customer_name=db.customer.Where(c=>c.city_code==customerid).Select(p=>p.customer_name).FirstOrDefault();
                }
                tick.list = list;

                return tick;
            }


            return tick;
        }

        private string fillStatus(int p)
        {
            if (p == 0)
                return "UnAssigned";
            if (p == 1)
                return "Pending";
            if (p == 2)
                return "InProgress";
            if (p == 3)
                return "Complete";
            else
                return "Closed";
        }
    }
}
