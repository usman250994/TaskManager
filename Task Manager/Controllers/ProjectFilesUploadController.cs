using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.Project;
using Task_Manager.viewModels.ProjectViewModel;
using System.Text.RegularExpressions;

namespace Task_Manager.Controllers
{
    public class ProjectFilesUploadController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpPost]
        public String UploadFiles(Files file)
        {
            var sessionId = HttpContext.Current.Session;
            int pid = Convert.ToInt32(sessionId["project"]);//projectid
            string filecode = file.fileCode.ToString();
            int uid = Convert.ToInt32(sessionId["UserID"]); //userid
            string subPath = "ImagesPath"; // your code goes here



            Files addFile = new Files();
            addFile.createdBy = db.user.Find(uid);
            addFile.filetype = file.fileCode;
            addFile.createdOn = DateTime.Now;
            db.files.Add(addFile);
            if (db.SaveChanges() > 0)
            {
                var id = addFile.id;
                addFile.fileCode = pid + filecode + id;
                var projectName = db.project.Find(pid).Project_Name;
                int workOrder = db.project.Find(pid).work_order;
                string pname = Regex.Replace(projectName, @"[^0-9a-zA-Z]+", " ");

                var path = HttpContext.Current.Server.MapPath("/Files/" + pname + "_W" + workOrder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                addFile.fileName = path + "\\" + pid + filecode + id + "." + file.fileName.Split('.')[1];
                db.SaveChanges();
                db.project.Find(pid).projectFiles.Add(addFile);
                db.SaveChanges();
                return "File Saved";
            }
            else
            {
                return "File Not Saved";
            }
        }

        [HttpGet]
        public object[] ViewFiles(int id)
        {
            var sessionId = HttpContext.Current.Session;
            int pid = Convert.ToInt32(sessionId["project"]);//projectid
            int uid = Convert.ToInt32(sessionId["UserID"]); //userid
            Names name = new Names();
            name.pname = db.project.Where(p => p.id == pid).Select(p => p.Project_Name).FirstOrDefault();
            name.cname = db.project.Where(p => p.id == pid).Select(p => p.customer.customer_name).FirstOrDefault();
            name.workorder = db.project.Where(p => p.id == pid).Select(p => p.work_order).FirstOrDefault();
            if (id == 0)
            {
                List<TeamTab> toReturn = new List<TeamTab>();
                List<int> userPresent = new List<int>();
                //List<Users> lists = new List<Users>();
                List<List<Users>> list = new List<List<Users>>();

                var tasks = db.tagging.Where(o => o.project.id == pid && o.tasks.IsTicket == false).Select(i => i.tasks.id).ToList();
                list = db.tagging.Where(p => p.project.id == pid && tasks.Contains(p.tasks.id)).Select(o => o.users).ToList();
                foreach (var entity in list)
                {
                    foreach (var ent in entity)
                    {
                        if (userPresent.IndexOf(ent.id) == -1)
                        {
                            TeamTab team = new TeamTab();
                            team.Address = ent.Address;
                            team.Name = ent.user_Name;
                            team.Number = ent.MobileNo;
                            toReturn.Add(team);
                            userPresent.Add(ent.id);
                        }
                    }
                }
                return new Object[] { toReturn, name };
            }
            else if (id == 1)
            {
                List<taskTab> toReturn = new List<taskTab>();
                List<Task> list = new List<Task>();
                list = db.tagging.Where(p => p.project.id == pid && p.tasks.IsTicket == false).Select(o => o.tasks).ToList();
                foreach (var entity in list)
                {
                    taskTab task = new taskTab();
                    task.task_No = entity.id;
                    task.task_Name = entity.task_name;
                    task.start_Date = entity.start_date.Date.ToShortDateString();
                    task.end_Date = entity.end_date.Date.ToShortDateString(); ;
                    toReturn.Add(task);
                }
                return new Object[] { toReturn };
            }
            else if (id == 2)
            {
                List<FilesTab> toReturn = new List<FilesTab>();
                List<Files> list = new List<Files>();
                list = db.project.Where(p => p.id == pid).Select(o => o.projectFiles).FirstOrDefault();
                foreach (var entity in list)
                {

                    FilesTab files = new FilesTab();
                    files.fileCode = entity.fileCode;
                    files.file_type_name = db.filetype.Where(s => s.Filecode == entity.filetype).Select(s => s.Filename).FirstOrDefault();
                    files.uploaded_user = entity.createdBy.user_Name;
                    files.uploaded_date = entity.createdOn.Date.ToShortDateString();
                    files.uploaded_time = entity.createdOn.ToShortTimeString();
                    string file = entity.fileName;

                    file = file.Substring(file.LastIndexOf("Files"));
                    file = file.Replace(" ", "%20");
                    files.Download = "<a id='" + entity.id + "' href=" + @"\" + file + " download=" + entity.fileCode + "><p> Download</p></a>";
                    toReturn.Add(files);
                }
                return new Object[] { toReturn };
            }
            else
            {
                List<TicketTab> toReturn = new List<TicketTab>();
                var list = db.tagging.Where(p => p.project.id == pid && p.tasks.IsTicket == true).Select(o => o.tasks).ToList();
                foreach (var entity in list)
                {
                    TicketTab files = new TicketTab();
                    files.ticket_code = entity.ticket_code.ToString();
                    files.ticket_Name = entity.task_name;
                    files.branch = entity.branch_code;
                    files.description = entity.description;
                    toReturn.Add(files);
                }
                return new Object[] { toReturn };
            }
        }

    }
}