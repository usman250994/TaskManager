using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels.response;

namespace Task_Manager.Controllers
{
    public class taskSortingApiController : ApiController
    {
        TaskContext db = new TaskContext();

        [HttpGet]
        public List<taskResponse> ticketkByStatus(int id)
        {
            List<Task> tasks = new List<Task>();

            List<taskResponse> taskResponse = new List<taskResponse>();

            var session = HttpContext.Current.Session;
            var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));

            if (session["UserID"].ToString() == "5")
            {
                tasks = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == id).ToList();
            }
            else
            {

                tasks = db.task.Where(c => c.enable == true && c.Created_By.id == createdBy.id && c.IsTicket == true && c.status == id).ToList();

                //
                var tempTask = db.task.Where(c => c.enable == true && c.Created_By.id != createdBy.id && c.IsTicket == true && c.status == id).ToList();
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

                if (session["role_id"].ToString() == "1" || session["role_id"].ToString() == "2")
                {

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
                }
                else
                {
                    if (tasks[i].status == 0)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0' selected >Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 1)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1' selected >Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 2)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2' selected >InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 3)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3' selected >Complete</option><option value='4'>Closed</option></select>";
                    else
                        taskRes.status = "Closed";
                }

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

        [HttpPut]
        public List<taskResponse> taskByStatus(int id)
        {
            List<Task> tasks = new List<Task>();

            List<taskResponse> taskResponse = new List<taskResponse>();

            var session = HttpContext.Current.Session;
            var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));

            if (session["UserID"].ToString() == "5")
            {
                tasks = db.task.Where(c => c.enable == true && c.IsTicket == false && c.status == id).ToList();
            }
            else
            {

                tasks = db.task.Where(c => c.enable == true && c.Created_By.id == createdBy.id && c.IsTicket == false
                    && c.status == id).ToList();

                //
                var tempTask = db.task.Where(c => c.enable == true && c.Created_By.id != createdBy.id && c.IsTicket == false && c.status == id).ToList();
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

                if (session["role_id"].ToString() == "1" || session["role_id"].ToString() == "2")
                {

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
                }
                else
                {
                    if (tasks[i].status == 0)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0' selected >Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 1)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1' selected >Pending</option><option value='2'>InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 2)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2' selected >InProgress</option><option value='3'>Complete</option><option value='4'>Closed</option></select>";
                    else if (tasks[i].status == 3)
                        taskRes.status = @"<select  onchange='status(this.value," + tasks[i].id + ")' id=" + tasks[i].id + "><option value='0'>Unassigned</option><option value='1'>Pending</option><option value='2'>InProgress</option><option value='3' selected >Complete</option><option value='4'>Closed</option></select>";
                    else
                        taskRes.status = "Closed";
                }

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
    }
}
