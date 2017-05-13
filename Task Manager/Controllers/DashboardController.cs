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
            if (Session["UserId"] != null)
            {
                int unassigned = 0;
                int todays = 0;
                int newly = 0;
                List<Task> tasks = new List<Task>();
                //
                
                DateTime date = DateTime.Now.Date;
              var   task = db.task.Where(c => c.enable == true).ToList();
                foreach (var entity in task)
                {
                    if (entity.created_on.Date == date)
                    {
                        tasks.Add(entity);
                    }

                }
                todays = tasks.Count;
                ViewData["todays"] = todays;
                tasks.Clear();
                //Unassigned

                tasks = db.task.Where(c => c.enable == true && c.status==0).ToList();
              
               ViewData["unassigned"] = tasks.Count;
               tasks.Clear();
                //Newly

                tasks = db.task.Where(c => c.enable == true).Take(10).ToList();
                newly = tasks.Count;
                ViewData["newly"] = newly;
                tasks.Clear();
                //
                return View();
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}