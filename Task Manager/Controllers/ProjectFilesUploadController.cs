using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.Project;
using Task_Manager.viewModels.ProjectViewModel;

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
            string uid = sessionId["UserID"].ToString(); //userid
            var createdUser = db.user.Find(Convert.ToInt32(uid));
            Files addFile = new Files();
            addFile.filetype = file.fileCode;
            addFile.createdOn = DateTime.Now;

            db.files.Add(addFile);
            if (db.SaveChanges() > 0)
            {
                var id = addFile.id;
                addFile.fileCode = pid + filecode + id;
                addFile.fileName = "/Files/" + id + "." + file.fileName.Split('.')[1];
                //   sessionId["project"] = "1";
                //  addFile.fileName = "/Image/" + id;
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
            Names name = new Names();
            name.pname = db.project.Where(p => p.id == pid).Select(p => p.Project_Name).FirstOrDefault();
            name.cname = db.project.Where(p => p.id == pid).Select(p => p.customer.customer_name).FirstOrDefault();
            if (id == 0)
            {
                List<TeamTab> toReturn = new List<TeamTab>();
                List<int> userPresent = new List<int>();
                //List<Users> lists = new List<Users>();
                List<List<Users>> list = new List<List<Users>>();

                list = db.tagging.Where(p => p.project.id == pid).Select(o => o.users).ToList();
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
               
                
                return new Object[]{toReturn, name};
               
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
                return new Object[] { toReturn};
            }
            else
            {
                List<FilesTab> toReturn = new List<FilesTab>();

                List<Files> list = new List<Files>();

                list = db.project.Where(p => p.id == pid).Select(o => o.projectFiles).FirstOrDefault();


                foreach (var entity in list)
                {
                    FilesTab files = new FilesTab();
                    files.fileCode = entity.fileCode;
                    files.file_type_name = db.filetype.Where(s => s.Filecode == entity.filetype).Select(s => s.Filename).FirstOrDefault();
                    files.uploaded_date = entity.createdOn.Date.ToShortDateString();
                    files.uploaded_time = entity.createdOn.ToShortTimeString();
                    files.Download = "<a id='" + entity.id + "' href=" + entity.fileName + " download=" + entity.id + "><p> Download</p></a>";
                    toReturn.Add(files);
                }
                return new Object[] { toReturn };
            }
           
        }
         
    }
}