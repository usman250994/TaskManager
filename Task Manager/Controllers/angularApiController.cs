using System;
using System.Collections.Generic;
using System.IO;
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
                    taggedUsers.Add(new tagUsersView { id = entity.id, name = entity.user_Name });
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


        [HttpDelete]
        public void statusoftask(status stat)
        {
            // **************************** Adding File In StatusDocument DB*************
            var Session = HttpContext.Current.Session;
            int userId = Convert.ToInt32(Session["UserID"]);
            Session["project"] = stat.user;
            if (stat.filenames != "")
            {
                StatusDocuments statusDoc = new StatusDocuments();
                statusDoc.id = 0;
                statusDoc.createdBy = db.user.Find(userId);
                statusDoc.createdOn = DateTime.Now;
                if (stat.value == 4)
                {
                    statusDoc.isClose = true;
                }
                else
                {
                    statusDoc.isClose = false;
                }
                db.ticketdocuments.Add(statusDoc);
                db.task.Find(stat.user).statusDocument.Add(statusDoc);
                if (db.SaveChanges() > 0)
                {
                    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/TicketNotes/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    statusDoc.documentPath = path + "Ticket_No_" + statusDoc.id + "." + stat.filenames.Split('.')[1];
                    db.SaveChanges();
                }

            }

            var task = db.task.Find(stat.user);
            task.status = stat.value;
            if (stat.note != "")
            {
                if (stat.value == 3)
                {
                    task.completeNote = stat.note;
                    task.completeDate = DateTime.Now;
                    task.completingUser = db.user.Find(userId);
                }
                else
                {
                    task.note = stat.note;
                    task.closingDate = DateTime.Now;
                    task.closingUser = db.user.Find(userId);
                }
            }
            else
            {
                task.completingUser = null;
                task.closingUser = null;
            }
            db.SaveChanges();
        }


        [HttpPost]
        public List<dropProd> gethem(int id)
        {
            List<dropProd> dropproject = new List<dropProd>();
            dropproject = db.project.Where(o => o.Enable == true).Select(o => new dropProd { name = o.Project_Name, id = o.id, custId = o.customer.customerId }).ToList();
            return dropproject;
        }


        [HttpPost]
        public List<object> find()
        {
            List<Object> list = new List<object>();
            List<tagUsersView> taguserlist = new List<tagUsersView>();
            taguserlist = db.user.Where(o => o.Enable == true).Select(o => new tagUsersView { id = o.id, name = o.user_Name }).ToList();
            list.Add(taguserlist);
            List<Category> categoryList = new List<Category>();
            categoryList = db.caterory.Where(p => p.enable == true).ToList();
            list.Add(categoryList);
            return list;


        }
    }
}
