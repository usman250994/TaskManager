﻿using System;
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
    public class TaskApiController : ApiController
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
                session.Clear();
                var task = db.task.Find(Convert.ToInt32(str));
                var pid = db.tagging.Where(p => p.tasks.id == task.id).Select(p => p.project.id).FirstOrDefault();

                returning.task = task;
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

        //Create Task
        [HttpPost]
        public String CreateTask([FromBody]tempTask tempTask)
        {

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

               
            }

            else
            {
                var task = setTask(tempTask);
               // db.task.Add(task);
                Tagging tag = new Tagging();
                tag.tasks = task;
                tag.project = db.project.Find(tempTask.projectId);
                List<Users> usr = new List<Users>();

                for (int i = 0; i < tempTask.tempUsers.Count; i++)
                {
                     var user = db.user.Find(tempTask.tempUsers[i]);
                     usr.Add(user);
                }
               tag.users = usr;
                db.tagging.Add(tag);

            }
            if (db.SaveChanges() > 0)
            {
                return "task success!!";

            }
            else
            {
                return "Some Error";
            }           
        }

        
        
        private Task setTask (tempTask tempTask)
        {
            Task task = new Task();
            task.enable = true;
            task.created_on = DateTime.Now;
            task.task_name = tempTask.task_name;
            task.description = tempTask.description;
            task.start_date = tempTask.start_date;
            task.end_date = tempTask.end_date;
            task.status = tempTask.status;
            task.sms = tempTask.sms;
            task.email = tempTask.email;
            task.id = tempTask.id;
            return task;
        }



        ////Deleting tasks without tagging
        [HttpPost]
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
            List<taskResponse> taskResponse = new List<taskResponse>();
    
            
            //checking for dashboard returns 
            
            var session = HttpContext.Current.Session;
            if (session["dashboard"] != null)
            {
                string str = session["dashboard"].ToString();
                session.Clear();
                
                //todays 
                if(str=="t")
                {
                    DateTime date=DateTime.Now.Date;
             var task  = db.task.Where(c => c.enable == true).ToList();
                    foreach( var entity in  task )
                    {
                        if (entity.created_on.Date == date)
                        {
                            tasks.Add(entity);
                        }
                        
                    }
                    
                }

                    //unassigned
                else if (str == "u")
                {
                    tasks = db.task.Where(c => c.enable == true).ToList();
                }

                //newly
                else
                {
                    tasks = db.task.Where(c => c.enable == true).Take(10).ToList();
                }
            }
                //all
            else
            {
                tasks = db.task.Where(c => c.enable == true).ToList();
            }
            
            for (int i = 0; i < tasks.Count; i++)
            {
                taskResponse taskRes = new taskResponse();
                taskRes.task = tasks[i];
                Tagging tag = new Tagging();
                List<string> list = new List<string>();

                tag = db.tagging.Find(tasks[i].id);
                //int k = tag.users.Count;

                if (tag != null && tag.users.Count > 0)
                {

                    for (int j = 0; j < tag.users.Count; j++)
                    {
                        string toAdd = tag.users[j].user_Name;
                        list.Add(toAdd);
                    }
                }
                else
                {
                    list.Clear();
                }
                taskRes.users = list;
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