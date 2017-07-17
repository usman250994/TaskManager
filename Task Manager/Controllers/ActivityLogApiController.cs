using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class ActivityLogApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpGet]
        public List<Activity> Userall()
        {
            List<Activity> toReturn = new List<Activity>();
            var list = db.auditLog.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                int perfomid = Convert.ToInt32(list[i].activityId);
                int user = Convert.ToInt32(list[i].createdBy);
                string tablename = list[i].tableName.ToString();
                Activity active = new Activity();
                active.actionName = list[i].actionCode;
                active.tableName = tablename;
                if (tablename == "Customer")
                {
                    active.Perform = db.customer.Where(p => p.customerId == perfomid).Select(p => p.customer_name).FirstOrDefault();
                }
                else if (tablename == "Project")
                {
                    active.Perform = db.project.Where(p => p.id == perfomid).Select(p => p.Project_Name).FirstOrDefault();
                }
                else if (tablename == "Users")
                {
                    active.Perform = db.user.Where(p => p.id == perfomid).Select(p => p.user_Name).FirstOrDefault();
                }
                else
                {
                    active.Perform = db.task.Where(p => p.id == perfomid).Select(p => p.task_name).FirstOrDefault();
                }
               
                active.User = db.user.Where(p => p.id == user).Select(p => p.user_Name).FirstOrDefault();
                active.Date = list[i].activityDate.ToString();
                toReturn.Add(active);
            }
            return toReturn;
        }

    }
}
