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

                returning.task = task;
                returning.tags = taggedUsers;
                returning.projectId = pid;
              //  returning.dropdowns = find();
                return returning;
            }
            else
            {
                return returning;

            }
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
