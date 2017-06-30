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
    public class messageApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpGet]
        public int View(int id)
        {
            var session = HttpContext.Current.Session;
            session["task_id"] = id;
            return 0;
        }

        [HttpGet]
        public responseListForMessage View()
        {
            responseListForMessage returnTo = new responseListForMessage();
            List<responseMessage> listRes = new List<responseMessage>();
            var session = HttpContext.Current.Session;
            var id = Convert.ToInt32(session["task_id"]);
            var objTask = db.task.Find(id);
            var msgs = db.task.Where(p => p.id == id).Select(p => p.discussion).FirstOrDefault();
            var leftRight = true;
            var sentById = 0;
            foreach (var entity in msgs)
            {
                responseMessage res = new responseMessage();
                res.name = entity.sentBy.user_Name;
                res.initials = entity.sentBy.user_Name.Substring(0, 2);
                res.timeStamp = entity.timeStamp;
                res.location = entity.location;
                res.message = entity.message;
                if (entity.sentBy.id != sentById)
                {
                    sentById = entity.sentBy.id;
                    if (leftRight == true)
                    {
                        res.direction = false;
                        leftRight = false;
                    }
                    else
                    {
                        res.direction = true;
                        leftRight = true;
                    }
                }
                else
                {
                    res.direction = leftRight;
                }
                listRes.Add(res);

            }

            var users = db.tagging.Where(p => p.tasks.id == id).Select(p => p.users.Select(q => q.user_Name).ToList()).FirstOrDefault();
            returnTo.taggedUsers = users;
            returnTo.responseList = listRes;
            returnTo.task_no = objTask.ticket_code;
            returnTo.task_name = objTask.task_name;
            var projid = db.tagging.Where(c => c.tasks.id == objTask.id).Select(p => p.project.id).FirstOrDefault();
            var obj = db.project.Where(p => p.id == projid).Select(c => c.customer).FirstOrDefault();
            returnTo.customercode = obj.city_code;
            returnTo.customername = obj.customer_name;
            returnTo.Description = objTask.description;
            returnTo.sDate = objTask.start_date.Date.ToShortDateString();
            returnTo.eDate = objTask.end_date.Date.ToShortDateString();
            returnTo.Email = objTask.email;
            returnTo.SMS = objTask.sms;
            returnTo.assigned = users.ToString();
            returnTo.createdby = objTask.Created_By.user_Name;
            returnTo.projectname = db.project.Where(p => p.id == projid).Select(p => p.Project_Name).FirstOrDefault();

            return returnTo;
        }


        [HttpPut]
        public int saveMessage(reqmessage req)
        {
            var session = HttpContext.Current.Session;
            var taskId = Convert.ToInt32(session["task_id"]);
            int userId = Convert.ToInt32(session["UserID"]);

            messages msg = new messages();

            msg.id = 0;
            msg.location = req.loc;
            msg.sentBy = db.user.Find(userId);
            msg.timeStamp = DateTime.Now;
            msg.message = req.msg;
            // var ts = db.task.Where(c => c.id == taskId).FirstOrDefault();
            // var ts = db.task.Find(taskId);
            db.messages.Add(msg);

            db.task.Find(taskId).discussion.Add(msg);
            //List<messages> ms = new List<messages>();
            //ms.Add(msg);
            //ts.discussion = ms;


            if (db.SaveChanges() > 0)
            {

                return 1;
            }
            else
                return 0;
        }
    }
}
