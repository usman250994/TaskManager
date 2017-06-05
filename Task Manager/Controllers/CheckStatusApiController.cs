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
        public List<clientsTicketRespone> statusReturn()
        {
            List<clientsTicketRespone> list = new List<clientsTicketRespone>();
            var Session = HttpContext.Current.Session;
            string customerid = Session["customerid"].ToString();
           
            int id = 0;
            if (!string.IsNullOrEmpty(customerid))
            {
                id = Convert.ToInt32(customerid);
                if (db.task.Any(o => o.id == id))
                {
                    clientsTicketRespone status = new clientsTicketRespone();

                    var ticket = db.task.Find(id);
                    status.id = ticket.id;
                    status.status = fillStatus(ticket.status);
                    status.date = ticket.created_on;
                    status.description = ticket.description;
                    status.name = ticket.task_name;
                    status.branch_code = ticket.branch_code;
                    status.project_name = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.Project_Name).FirstOrDefault();
                    status.code = customerid;
                    var projId = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.id).FirstOrDefault();
                    status.customer_name = db.project.Where(o => o.id == projId).Select(o => o.customer.customer_name).FirstOrDefault();
                    list.Add(status);
                    return list;

                }
                else
                {
                    var projIds = db.project.Where(p => p.customer.city_code == customerid).Select(p => p.id).ToList();
                    List<int> taskIdList = new List<int>();
                    foreach (var entity in projIds)
                    {
                        var lis = db.tagging.Where(p => p.project.id == entity && p.tasks.IsTicket == true).Select(p => p.tasks.id).ToList();

                        foreach (var li in lis)
                        {
                            var ticket = db.task.Find(li);
                            clientsTicketRespone status = new clientsTicketRespone();
                            status.id = ticket.id;

                            status.status = fillStatus(ticket.status);
                            status.date = ticket.created_on;
                            status.branch_code = ticket.branch_code; 
                            status.description = ticket.description;
                            status.name = ticket.task_name;
                            status.code = customerid;
                            status.project_name = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.Project_Name).FirstOrDefault();
                            var projId = db.tagging.Where(o => o.tasks.id == ticket.id).Select(o => o.project.id).FirstOrDefault();
                            status.customer_name = db.project.Where(o => o.id == projId).Select(o => o.customer.customer_name).FirstOrDefault();
                            list.Add(status);
                        }
                    }
                }


            }
            return list;

        }

        private string fillStatus(int p)
        {
            if (p == 1)
                return "Pending";
            if (p == 2)
                return "Progress";
            if (p == 3)
                return "Complete";
            else
                return "Resolved";
        }
    }
}
