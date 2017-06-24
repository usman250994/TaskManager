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
    public class TicketApiController : ApiController
    {
        TaskContext db = new TaskContext();

        // Grid Status Fill Dropdown
        [HttpPut]
        public String fillDropDown(status stat)
        {
            db.task.Find(stat.user).status = stat.value;
            if (db.SaveChanges() == 1)
            {
                return "DONE";
            }
            else
            {
                return "status failed";
            }
        }

        ////session set
        [HttpPut]
        public int set(int id)
        {
            var session = HttpContext.Current.Session;
            session["Task"] = id;
            return 0;
        }

        //gtetting session and dropdown info
        [HttpGet]
        public taskupdate get(int id)
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
                var pi = db.tagging.Where(o => o.tasks.id == task.id).FirstOrDefault();
                var pid = pi.project.id;
                var cid = db.project.Where(o => o.id == pid).Select(u => u.customer.customerId).FirstOrDefault();
                returning.task = task;
                returning.customerId = cid;
                returning.tags = taggedUsers;
                returning.projectId = pid;
                taskupdate taskUpd = new taskupdate();
                taskUpd.id = returning.task.id;
                taskUpd.taskname = returning.task.task_name;
                taskUpd.cid = returning.customerId;
                taskUpd.pid = returning.projectId;
                taskUpd.bCode = returning.task.branch_code;
                taskUpd.issue = returning.task.description;
                taskUpd.list = db.user.Where(o => o.Enable == true).Select(o => new tagUsersView { id = o.id, name = o.user_Name }).ToList();
                taskUpd.dropcustomer = db.customer.Where(o => o.enable == true).Select(o => new dropCust { name = o.customer_name, id = o.customerId }).ToList();
                taskUpd.dropproject = db.project.Where(o => o.Enable == true).Select(o => new dropProd { name = o.Project_Name, id = o.id, custId = o.customer.customerId }).ToList();
                taskUpd.tags = returning.tags;
                taskUpd.sms = returning.task.sms;
                taskUpd.email = returning.task.email;

                return taskUpd;
            }
            else
            {

                taskupdate taskUpd = new taskupdate();
                List<tagUsersView> taggedUsers = new List<tagUsersView>();
                taskUpd.tags = taggedUsers;
                taskUpd.list = db.user.Where(o => o.Enable == true).Select(o => new tagUsersView { id = o.id, name = o.user_Name }).ToList();
                taskUpd.dropcustomer = db.customer.Where(o => o.enable == true).Select(o => new dropCust { name = o.customer_name, id = o.customerId }).ToList();
                taskUpd.dropproject = db.project.Where(o => o.Enable == true).Select(o => new dropProd { name = o.Project_Name, id = o.id, custId = o.customer.customerId }).ToList();
                return taskUpd;
            }
        }

        //Create Ticket or update task
        [HttpPost]
        public String CreateTask([FromBody]taskupdate temp)
        {
            internalTickets temptask = new internalTickets();


            if (temp.id == 0)
            {
                temptask.created_on = DateTime.Now;
                temptask.Modified = DateTime.Now;

            }
            else
            {
                temptask.Modified = DateTime.Now;
            }


            temptask.id = temp.id;
            temptask.branchCode = temp.bCode;
            temptask.uname = temp.taskname;
            temptask.projectId = temp.pid;
            temptask.issue = temp.issue;
            temptask.email = temp.email;
            temptask.sms = temp.sms;
            temptask.assignedTo = temp.tags;

            //
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
            var createdUser = db.user.Find(Convert.ToInt32(id));

            //update
            if (temptask.id != 0)
            {

                var taskdetail = db.task.Find(temptask.id);
                temptask.created_on = taskdetail.created_on;
                temptask.sdate = taskdetail.start_date;
                temptask.edate = taskdetail.end_date;
                db.Entry(taskdetail).CurrentValues.SetValues(setTask(temptask));

                //updating project
                var tagging = db.tagging.Where(p => p.tasks.id == temptask.id).FirstOrDefault();
                var tag = tagging;
                tag.project = db.project.Find(temptask.projectId);
                db.Entry(tagging).CurrentValues.SetValues(tag);

                //adding users
                tagging.users.Clear();
                for (int i = 0; i < temptask.assignedTo.Count; i++)
                {
                    var user = db.user.Find(temptask.assignedTo[i].id);
                    tagging.users.Add(user);
                }
            }
            //create
            else
            {
   
                var task = setTask(temptask);
                // db.task.Add(task);
                Tagging tag = new Tagging();
                tag.tasks = task;
                tag.project = db.project.Find(temptask.projectId);
                List<Users> usr = new List<Users>();

                for (int i = 0; i < temptask.assignedTo.Count; i++)
                {
                    var user = db.user.Find(temptask.assignedTo[i].id);
                    usr.Add(user);
                }
                tag.users = usr;
                db.tagging.Add(tag);

            }
            if (db.SaveChanges() > 0)
            {
                return "ticket success!!";

            }
            else
            {
                return "Some Error";
            }
        }

        private Task setTask(internalTickets tempTask)
        {
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
        

            var createdUser = db.user.Find(Convert.ToInt32(id));
            Task task = new Task();
            task.enable = true;
            
            task.Created_By = createdUser;
            task.task_name = tempTask.uname;
            task.description = tempTask.issue;
            
            task.branch_code = tempTask.branchCode;
            task.IsTicket = true;
            task.created_on = tempTask.created_on;
            task.start_date = DateTime.Now;
            task.end_date = DateTime.Now;
            task.LastModify = tempTask.Modified;
            if (tempTask.assignedTo.Count == 0)
            {
                task.status = 0;
            }
            else
            {
                task.status = 2;
            }
            task.sms = tempTask.sms;
            task.email = tempTask.email;
            task.id = tempTask.id;

            var code = db.project.Where(p => p.id == tempTask.projectId).Select(o => o.customer.city_code).FirstOrDefault();

            code=code.Substring(2,4);
            task.ticket_code = db.task.Where(c=> c.IsTicket==true).Count()+code;
            return task;
        }

        //change return to a view model
        public responseToComplain TicketUser(int id)
        {
            responseToComplain tick = new responseToComplain();





            var contacts = db.tickets.Select(p => new finding_ticketContact() { tasks = p._tickets, number = p.contact, name = p.name, email = p.email }).ToList();
            foreach (var cont in contacts)
            {
                var obj = cont.tasks.Where(p => p.id == id).FirstOrDefault();

                if (obj != null)
                {
                    tick.email = cont.email;
                    tick.name = cont.name;
                    tick.number = cont.number;
                    break;
                }

            }
            return tick;

        }


        ////Deleting tasks without tagging
        [HttpDelete]
        public String delete(int id)
        {
            var user = db.task.Where(p => p.id == id).FirstOrDefault().enable = false;

            if (db.SaveChanges() == 1)
            {
                return "Task Deleted";
            }
            else
            {
                return "Task Not Deleted";
            }
        }

        //// For Grid Data
        [HttpGet]
        public List<taskResponse> taskall()
        {
            List<Task> tasks = new List<Task>();
            //List<Task> task = new List<Task>();
            List<taskResponse> taskResponse = new List<taskResponse>();
            //Checking for Dashboard returns 
            var session = HttpContext.Current.Session;
            var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
            if (session["dashboard"] != null)
            {
                string str = session["dashboard"].ToString();
               
                session["dashboard"] = null;
                //Todays 
                if (str == "tick_today")
                {
                    DateTime date = DateTime.Now.Date;


                    if (session["UserID"].ToString() == "5")
                    {
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.created_on>=date).ToList();
                    }
                    else
                    {
                        
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdBy.id && c.created_on>=date ).ToList();

                        //

                        var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id && c.created_on >= date).ToList();
                        foreach (var entity in tempTask)
                        {
                            var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                            for (int i = 0; i < tag.users.Count; i++)
                            {
                                if (tag.users[i].id == createdBy.id)
                                {
                                    tasks.Add(entity);
                                }
                                break;
                            }


                        }
                        //

                    }


                    //foreach (var entity in task)
                    //{
                    //    if (entity.created_on.Date == date)
                    //    {
                    //        tasks.Add(entity);
                    //    }
                    //}
                }
                //Unassigned
                else if (str == "tick_unassign")
                {

                    if (session["UserID"].ToString() == "5")
                    {
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status==0).ToList();
                    }
                    else
                    {
                       
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status==0 && c.Created_By.id == createdBy.id ).ToList();


                        //
                        //var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id).ToList();
                        //foreach (var entity in tempTask)
                        //{
                        //    var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                        //    for (int i = 0; i < tag.users.Count; i++)
                        //    {
                        //        if (tag.users[i].id == createdBy.id)
                        //        {
                        //            task.Add(entity);
                        //        }
                        //        break;
                        //    }
                        //}
                    }

                    //foreach (var entity in task)
                    //{
                    //    if (entity.status == 0)
                    //    {
                    //        tasks.Add(entity);
                    //    }
                    //}
                }
                //Newly
                else
                {
                    if (session["UserID"].ToString() == "5")
                    {
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == 2).ToList();
                    }
                    else
                    {
                       
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdBy.id && c.status==2).ToList();
                        int remaining = 10 - tasks.Count;
                        //
                        if (remaining > 0)
                        {
                            var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id && c.status==2).ToList();
                            foreach (var entity in tempTask)
                            {
                                var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                                for (int i = 0; i < tag.users.Count; i++)
                                {
                                    if (tag.users[i].id == createdBy.id)
                                    {
                                        tasks.Add(entity);
                                    }
                                    break;
                                }
                            }
                        }
                        //
                    }
                }
            }
            //all
            else
            {

                if (session["UserID"].ToString() == "5")
                {
                    tasks = db.task.Where(c => c.enable == true && c.IsTicket == true).ToList();
                }
                else
                {
                 
                    tasks = db.task.Where(c => c.enable == true && c.Created_By.id == createdBy.id && c.IsTicket == true).ToList();

                    //
                    var tempTask = db.task.Where(c => c.enable == true && c.Created_By.id != createdBy.id && c.IsTicket == true).ToList();
                    foreach (var entity in tempTask)
                    {
                        var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                        for (int i = 0; i < tag.users.Count; i++)
                        {
                            if (tag.users[i].id == createdBy.id)
                            {
                                tasks.Add(entity);
                                break;
                            }
                           
                        }
                    }
                    //
                }

            }
            for (int i = 0; i < tasks.Count; i++)
            {
                taskResponse taskRes = new taskResponse();
                taskRes.ticket_name = tasks[i].ticket_code;
                int ids = Convert.ToInt32(tasks[i].id);
                taskRes.task_name = tasks[i].task_name;
                taskRes.description = tasks[i].description;
                taskRes.email = tasks[i].email;
                taskRes.bran_Code = tasks[i].branch_code;
                taskRes.sms = tasks[i].sms;
                taskRes.createdBy = tasks[i].Created_By.user_Name;
                taskRes.lastModify = tasks[i].LastModify.Date.ToShortDateString();
                var projid = db.tagging.Where(c => c.tasks.id == ids).Select(p => p.project.id).FirstOrDefault();
                var obj = db.project.Where(p => p.id == projid).Select(c => c.customer).FirstOrDefault();
                taskRes.cCode = obj.city_code;
                taskRes.cName = obj.customer_name;


                if (tasks[i].end_date == null)
                    taskRes.endDate = "--";
                else
                    taskRes.endDate = tasks[i].end_date.Date.ToShortDateString();
                if (tasks[i].start_date == null)
                    taskRes.startDate = "--";
                else
                    taskRes.startDate = tasks[i].start_date.Date.ToShortDateString();

                if (tasks[i].status == 0)
                    taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0' selected >Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                else if (tasks[i].status == 1)
                    taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1' selected >Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                else if (tasks[i].status == 2)
                    taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2' selected >InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                else if (tasks[i].status == 3)
                    taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3' selected >Complete</option><option value='4'>Closed</option></select>";
                else
                    taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4' selected>Closed</option></select>";


                taskRes.button = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='UpdateTask(" + tasks[i].id + ")'/><button  class='btn btn-danger fa fa-times' onclick='DeleteTask(" + tasks[i].id + "," + i + ")'></button> <button  class='btn btn-info fa fa-comments-o' onclick='comments(" + tasks[i].id + ")'></button>";
                Tagging tag = new Tagging();
                List<string> list = new List<string>();
                int s = tasks[i].id;

                tag = db.tagging.Where(p => p.tasks.id == s).FirstOrDefault();
                if (tag != null && tag.users.Count > 0)
                {

                    for (int j = 0; j < tag.users.Count; j++)
                    {
                        string toAdd = tag.users[j].user_Name;
                        list.Add(toAdd);
                        taskRes.users = taskRes.users + toAdd + ",";
                    }
                }
                else
                {
                    list.Clear();
                }
                // taskRes.users = list;
                taskResponse.Add(taskRes);

            }
            return taskResponse;
        }

        private TaskDropdown find()
        {
            TaskDropdown tasksDropDown = new TaskDropdown();
            List<Project> projects = new List<Project>();
            projects = db.project.Where(p => p.Enable == true).ToList();
            tasksDropDown.projects = projects;

            List<Users> users = new List<Users>();
            users = db.user.Where(p => p.Enable == true).ToList();
            tasksDropDown.Users = users;

            List<Customer> cus = new List<Customer>();
            cus = db.customer.Where(p => p.enable == true).ToList();
            tasksDropDown.customer = cus;

            return tasksDropDown;

        }


    }
}