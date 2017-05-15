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
            var str = Session["UserId"];
            if (Session["UserId"] != null)
            {
                int unassigned = 0;
                int todays = 0;
                int newly = 0;
        
                
                List<Task> tasks = new List<Task>();

                //

                string id = Session["UserID"].ToString();
                var createdUser = db.user.Find(Convert.ToInt32(id));
                DateTime date = DateTime.Now.Date;
                List<Task> task = new List<Task>();
                if (id == "5")
                {
                    task = db.task.Where(c => c.enable == true).ToList();
                }
                else
                {
                     task = db.task.Where(c => c.enable == true && c.Created_By.id == createdUser.id).ToList();
                }

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
                if (id == "5")
                {
                    tasks = db.task.Where(c => c.enable == true && c.status == 0).ToList();
                }
                else
                {
                    tasks = db.task.Where(c => c.enable == true && c.status == 0 && c.Created_By.id == createdUser.id).ToList();
                }


                ViewData["unassigned"] = tasks.Count;
                tasks.Clear();
                //Newly
                if (id == "5")
                {
                    tasks = db.task.Where(c => c.enable == true).Take(10).ToList();
                }
                else
                {
                    tasks = db.task.Where(c => c.enable == true && c.Created_By.id == createdUser.id).Take(10).ToList();
                }

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