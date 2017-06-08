using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.response;

namespace Task_Manager.Controllers
{
    public class angularApiController : ApiController
    {
        TaskContext db = new TaskContext();

        [HttpGet]
        public updateTaskReturning get()
        {
            updateTaskReturning returning = new updateTaskReturning();
            var session = HttpContext.Current.Session;
            if (session["Task"] != null)
            {
                string str = session["Task"].ToString();
                session["Task"] = null;
                var task = db.task.Find(Convert.ToInt32(str));
                
                //
                List<tagUsersView> taggedUsers = new List<tagUsersView>();
                var tag = db.tagging.Where(p => p.tasks.id == task.id).FirstOrDefault();

                foreach (var entity in tag.users)
                {
                    taggedUsers.Add(new tagUsersView { id = entity.id, name = entity.user_Name} );
                }
                //
                var pid = db.tagging.Where(p => p.tasks.id == task.id).Select(p => p.project.id).FirstOrDefault();
                var cid = db.project.Where(o => o.id == pid).Select(u => u.customer.customerId).FirstOrDefault();
                returning.task = task;
                returning.customerId = cid;
                    returning.tags = taggedUsers;
               
                returning.projectId = pid;
              //  returning.dropdowns = find();
                return returning;
            }
            else
            {
                List<tagUsersView> taged = new List<tagUsersView>();
                returning.tags = taged;
                return returning;

            }
        }


        [HttpGet]
        public List<dropCust> getthem(int id)

        {
            List<dropCust> dropcustomer = new List<dropCust>();

            dropcustomer = db.customer.Where(o => o.enable == true).Select(o => new dropCust { name = o.customer_name, id = o.customerId }).ToList();
         
      return dropcustomer;
        }


        [HttpPost]
        public List<dropProd> gethem(int id)
        {
            List<dropProd> dropproject = new List<dropProd>();

            dropproject = db.project.Where(o => o.Enable == true).Select(o => new dropProd { name = o.Project_Name, id = o.id,custId=o.customer.customerId }).ToList();

            return dropproject;
        }


        [HttpPost]
        public List<tagUsersView> find()
        {
            List<tagUsersView> list = new List<tagUsersView>();

           list= db.user.Where(o => o.Enable == true).Select(o => new tagUsersView{id=o.id,name=o.user_Name}).ToList();
            return list;

        }
    }
}
