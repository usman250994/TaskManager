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
            var roles_Id = Session["role_id"].ToString();
            ViewData["id"] = roles_Id;
            if (Session["UserId"] != null)
            {
                List<Task> tasks = new List<Task>();
                //
                string id = Session["UserID"].ToString();
                var createdUser = db.user.Find(Convert.ToInt32(id));
                DateTime date = DateTime.Now.Date;
                List<Task> task = new List<Task>();
                if (Session["role_Id"].ToString() == "1")
                {
                    //todays
                    ViewData["todays"] =     db.task.Where(c => c.enable == true && c.IsTicket == false).Take(10).ToList().Count();
                //unassigned
                    ViewData["unassigned"] = db.task.Where(c => c.enable == true && c.IsTicket == false && c.status == 0).ToList().Count();
                //newly
                    ViewData["newly"] = db.task.Where(c => c.enable == true && c.IsTicket == false && c.created_on == date).ToList().Count();

                    //tickets
                    //todays
                    ViewData["todayst"] = db.task.Where(c => c.enable == true && c.IsTicket == true).Take(10).ToList().Count();
                    //unassigned
                    ViewData["unassignedt"] = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == 0).ToList().Count();
                    //newly
                    ViewData["newlyt"] = db.task.Where(c => c.enable == true && c.IsTicket == true && c.created_on == date).ToList().Count();
                }
                else
                {
                    //todays
                    ViewData["todays"] = db.task.Where(c => c.enable == true && c.IsTicket==false && c.Created_By.id == createdUser.id && c.created_on==date).Take(10).ToList().Count();
               //unassigned
                    ViewData["unassigned"] = db.task.Where(c => c.enable == true && c.IsTicket == false && c.status == 0 && c.Created_By.id == createdUser.id).ToList().Count();
                    //newly
                    ViewData["newly"] = db.task.Where(c => c.enable == true && c.IsTicket == false && c.Created_By.id == createdUser.id && c.created_on == date).ToList().Count();

                    //tickets
                    //todays
                    ViewData["todayst"] = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdUser.id && c.created_on == date).Take(10).ToList().Count();
                    //unassigned
                    ViewData["unassignedt"] = db.task.Where(c => c.enable == true && c.IsTicket == true && c.status == 0 && c.Created_By.id == createdUser.id).ToList().Count();
                    //newly
                    ViewData["newlyt"] = db.task.Where(c => c.enable == true && c.IsTicket == true && c.Created_By.id == createdUser.id && c.created_on == date).ToList().Count();
                }                 
                
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
    }
}