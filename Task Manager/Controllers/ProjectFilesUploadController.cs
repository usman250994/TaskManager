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

namespace Task_Manager.Controllers
{
    public class ProjectFilesUploadController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpPost]
        public String UploadFiles(files file)
        {
            var sessionId = HttpContext.Current.Session;
            string id = sessionId["UserID"].ToString();
            var createdUser = db.user.Find(Convert.ToInt32(id));
            files addFile = new files();
            addFile.fileName = file.fileName;
            addFile.fileCode = file.fileCode;
            addFile.createdOn = DateTime.Now;
            addFile.createdBy = createdUser;
            db.files.Add(addFile);
            if (db.SaveChanges() > 0)
            {
                return "File Saved";
            }
            else
            {
                return "File Not Saved";
            }
        }

        [HttpGet]
        public ProjectFileView ViewFiles()
        {
            ProjectFileView toReurn = new ProjectFileView();
            List<taskTab> task = new List<taskTab>();
            task = db.task.Where(p => p.enable == true && p.IsTicket == false).
                Select(x => new taskTab
                {
                    task_No = x.id,
                    task_Name = x.task_name,
                    start_Date = x.start_date,
                    end_Date = x.end_date
                }).ToList();
            toReurn.tasktab = task;
            List<FilesTab> filetab = new List<FilesTab>();
            files file = new files();
            // usman Query
            
            //
            return toReurn;
        }

    }
}