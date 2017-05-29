using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        TaskContext db = new TaskContext();
        public ActionResult Index()
        {
            Session["task_id"] = null;
            var str = Session["UserId"];
            var roles_Id = Session["role_id"].ToString();
            ViewData["id"] = roles_Id;
            if (Session["UserId"] != null)
            {
                List<int> view = new List<int>();
                List<Task> tasks = new List<Task>();
                //
                string id = Session["UserID"].ToString();
                var createdUser = db.user.Find(Convert.ToInt32(id));
                DateTime date = DateTime.Now.Date;
                List<Task> task = new List<Task>();
                if (Session["role_Id"].ToString() == "1")
                {
                    //todays
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == false && c.created_on >= date).ToList().Count());
                    //unassigned
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == false && c.status == 0).ToList().Count());
                    //newly
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == false).Take(20).ToList().Count());
                    //tickets
                    //todays
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == true && c.created_on >= date).ToList().Count());
                    //unassigned
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == 0).ToList().Count());
                    //newly
                    view.Add(db.task.Where(c => c.enable == true && c.IsTicket == true && c.created_on >= date).Take(20).ToList().Count());
                }
                else
                {
                    //todays 
                   var tids= db.task.Where(c => c.enable == true && c.IsTicket == false && c.created_on >= date).Select(p=>p.id).ToList();
                  
                    view.Add(callTasks(tids,createdUser.id));
                    
                    //unassigned
                    
                    view.Add( db.task.Where(c => c.enable == true && c.IsTicket == false && c.status == 0 && c.Created_By.id == createdUser.id).ToList().Count());
                    
                    //newly
                   tids= db.task.Where(c => c.enable == true && c.IsTicket == false).OrderByDescending(p => p.created_on).Select(p=>p.id).ToList();
                   view.Add(callTasks10(tids, createdUser.id));
                    
                    //tickets
                    //todayst
                    tids =db.task.Where(c => c.enable == true && c.IsTicket == true && c.created_on >= date).Select(p=>p.id).ToList();
                    view.Add(callTasks(tids, createdUser.id));
                    //unassignedt
                    view.Add( db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == 0 && c.Created_By.id == createdUser.id).ToList().Count());
                    //newlyt
                    tids = db.task.Where(c => c.enable == true && c.IsTicket == true).OrderByDescending(p => p.created_on).Select(p => p.id).ToList();
                    view.Add(callTasks10(tids, createdUser.id));
                }
             ViewBag.newly = view[0];
             ViewBag.unassigned = view[1];
             ViewBag.todays = view[2];
             ViewBag.todayst = view[3];
             ViewBag.unassignedt = view[4];
             ViewBag.newlyt = view[5];
                //foreach (var entity in tasks)
                //{
                //    if (entity.created_on.Date == date)
                //    {
                //        tasks.Add(entity);
                //    }
                //}
                ///  working for tickets
                ///  
                //ended working for tickets 
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private int callTasks(List<int> tids,int cUiId)
        {
            var count = 0;
            foreach(var id in tids)
            {
               if(db.task.Any(p => p.id == id && p.Created_By.id == cUiId))
               {
                   count++;
               }
            
                if (db.tagging.Any(p => p.tasks.id == id && p.users.Any(o => o.id == cUiId)))
                {
                    count++;
                }
               
            }

            return count;
        }

        private int callTasks10(List<int> tids, int cUiId)
        {
            var count = 0;
            foreach (var id in tids)
            {
                if (db.task.Any(p => p.id == id && p.Created_By.id == cUiId))
                {
                    count++;
                }

                if (db.tagging.Any(p => p.tasks.id == id && p.users.Any(o => o.id == cUiId)))
                {
                    count++;
                }
                if(count==10)
                {
                    return 10;
                }
            }

            return count;
        }
    }
}