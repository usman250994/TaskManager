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
        public List<responseMessage> View()
        {
            List<responseMessage> listRes = new List<responseMessage>();
            responseMessage res = new responseMessage();

            var session = HttpContext.Current.Session;
                var id = Convert.ToInt32(session["task_id"]);

                var msgs = db.task.Find(id).discussion.OrderByDescending(d=>d.timeStamp);
                var leftRight = true;
                var sentById = 0;
            foreach(var entity in msgs)
            {
                res.name = entity.sentBy.user_Name;
                res.initials = entity.sentBy.user_Name.Substring(0,2);
                res.timeStamp = entity.timeStamp;
                res.location = entity.location;
                if (entity.sentBy.id ==sentById)
                {
                    res.direction = leftRight;
                }
                else
                {
                   sentById = entity.sentBy.id;
                  
                    if(leftRight==true)
                    {
                  res.direction=false;
                  leftRight=false;
                    }
                    else
                    {
                        res.direction = false;
                        leftRight = true;
                    }

                }
                listRes.Add(res);

                }
            return listRes;
        }


          [HttpPut]
        public int saveMessage(reqmessage req)
        {
            var session = HttpContext.Current.Session;
          var taskId= Convert.ToInt32(session["task_id"]);
          var userId = Convert.ToInt32(session["user_Id"]);

          messages msg = new messages();

                  msg.id = 0;
                  msg.location=req.loc;
                  msg.sentBy=db.user.Find(userId);
                  msg.timeStamp=DateTime.Now;
                  msg.message=req.msg;

                  db.messages.Add(msg);

              if(db.SaveChanges()>0)

              { 
                  return 1; 
              }
              else
            return 0;
        }
    }
}
