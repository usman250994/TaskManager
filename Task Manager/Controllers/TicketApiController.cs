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
        [Route("/api/TaskApi/"), HttpPut]
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
        [Route("/api/TaskApi/"), HttpPut]
        public int set(int id)
        {
            var session = HttpContext.Current.Session;
            session["Task"] = id;
            return 0;
        }

        //gtetting session and dropdown info
        [HttpGet]
        public updateTaskReturning get(int id)
        {
            updateTaskReturning returning = new updateTaskReturning();
            var session = HttpContext.Current.Session;
            if (session["Task"] != null)
            {
                string str = session["Task"].ToString();
                session["Task"] = null;
                var task = db.task.Find(Convert.ToInt32(str));
                //
                List<int> taggedUsers = new List<int>();
                var tag = db.tagging.Where(p => p.tasks.id == task.id).FirstOrDefault();

                foreach (var entity in tag.users)
                {
                    taggedUsers.Add(entity.id);
                }
                //
                var pid = db.tagging.Where(p => p.tasks.id == task.id).Select(p => p.project.id).FirstOrDefault();

                returning.task = task;
                returning.tags = taggedUsers;
                returning.projectId = pid;
                returning.dropdowns = find();
                return returning;
            }
            else
            {
                returning.dropdowns = find();
                return returning;
            }
        }

        //Create Ticket or update task
        [HttpPost]
        public String CreateTask([FromBody]internalTickets tempTask)
        {
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
            var createdUser = db.user.Find(Convert.ToInt32(id));

            //update
            if (tempTask.id != 0)
            {

                var taskdetail = db.task.Find(tempTask.id);


                db.Entry(taskdetail).CurrentValues.SetValues(setTask(tempTask));

                //updating project
                var tagging = db.tagging.Where(p => p.tasks.id == tempTask.id).FirstOrDefault();
                var tag = tagging;
                tag.project = db.project.Find(tempTask.projectId);
                db.Entry(tagging).CurrentValues.SetValues(tag);

                //adding users
                tagging.users.Clear();
                for (int i = 0; i < tempTask.assignedTo.Count; i++)
                {
                    var user = db.user.Find(tempTask.assignedTo[i]);
                    tagging.users.Add(user);
                }



            }
            //create
            else
            {

                var task = setTask(tempTask);
                // db.task.Add(task);
                Tagging tag = new Tagging();
                tag.tasks = task;
                tag.project = db.project.Find(tempTask.projectId);
                List<Users> usr = new List<Users>();

                for (int i = 0; i < tempTask.assignedTo.Count; i++)
                {
                    var user = db.user.Find(tempTask.assignedTo[i]);
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
            task.created_on = DateTime.Now;
            task.Created_By = createdUser;
            task.task_name = tempTask.uname;
            task.description = tempTask.issue;
            task.start_date = DateTime.Now;
            task.end_date = DateTime.Now;
            task.IsTicket = true;

            if (tempTask.assignedTo.Count == 0)
            {
                task.status = 0;
            }
            else
            {
                task.status = 1;
            }
            task.sms = tempTask.sms;
            task.email = tempTask.email;
            task.id = tempTask.id;
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
        [Route("/api/TaskApi"), HttpGet]
        public List<taskResponse> taskall()
        {
            List<Task> tasks = new List<Task>();
            List<Task> task = new List<Task>();
            List<taskResponse> taskResponse = new List<taskResponse>();
            //Checking for Dashboard returns 
            var session = HttpContext.Current.Session;
          
            if (session["dashboard"] != null)
            {
                string str = session["dashboard"].ToString();
                session["dashboard"] = null;
                //Todays 
                if (str == "tick_today")
                {
                    DateTime date = DateTime.Now.Date;


                    if (session["UserID"].ToString() == "1")
                    {
                        task = db.task.Where(c => c.enable == true && c.IsTicket == true).ToList();
                    }
                    else
                    {
                        var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                        task = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdBy.id).ToList();

                        //

                        var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id).ToList();
                        foreach (var entity in tempTask)
                        {
                            var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                            for (int i = 0; i < tag.users.Count; i++)
                            {
                                if (tag.users[i].id == createdBy.id)
                                {
                                    task.Add(entity);
                                }
                                break;
                            }


                        }
                        //

                    }


                    foreach (var entity in task)
                    {
                        if (entity.created_on.Date == date)
                        {
                            tasks.Add(entity);
                        }
                    }
                }
                //Unassigned
                else if (str == "tick_unassign")
                {

                    if (session["UserID"].ToString() == "5")
                    {
                        task = db.task.Where(c => c.enable == true && c.IsTicket == true).ToList();
                    }
                    else
                    {
                        var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                        task = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdBy.id).ToList();


                        //
                        var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id).ToList();
                        foreach (var entity in tempTask)
                        {
                            var tag = db.tagging.Where(p => p.tasks.id == entity.id).FirstOrDefault();

                            for (int i = 0; i < tag.users.Count; i++)
                            {
                                if (tag.users[i].id == createdBy.id)
                                {
                                    task.Add(entity);
                                }
                                break;
                            }
                        }
                    }

                    foreach (var entity in task)
                    {
                        if (entity.status == 0)
                        {
                            tasks.Add(entity);
                        }
                    }
                }
                //Newly
                else
                {
                    if (session["UserID"].ToString() == "5")
                    {
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true).Take(10).ToList();
                    }
                    else
                    {
                        var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                        tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdBy.id).OrderByDescending(c => c.created_on).Take(10).ToList();
                        int remaining = 10 - tasks.Count;
                        //
                        if (remaining > 0)
                        {
                            var tempTask = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id != createdBy.id).OrderByDescending(c => c.created_on).Take(remaining).ToList();
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

                if (session["UserID"].ToString() == "1")
                {
                    tasks = db.task.Where(c => c.enable == true && c.IsTicket == true).ToList();
                }
                else
                {
                    var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                    tasks = db.task.Where(c => c.enable == true && c.Created_By.id == createdBy.id && c.IsTicket==true).ToList();

                    //
                    var tempTask = db.task.Where(c => c.enable == true &&  c.Created_By.id != createdBy.id && c.IsTicket == true).ToList();
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

            }
            for (int i = 0; i < tasks.Count; i++)
            {
                taskResponse taskRes = new taskResponse();
                taskRes.task = tasks[i];
                Tagging tag = new Tagging();
                List<string> list = new List<string>();
                int s = tasks[i].id;
                //  tag = db.tagging.Find(tasks[i].id);
                //  commenting for some time IReadOnlyCollection ^^
                //int k = tag.users.Count;
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