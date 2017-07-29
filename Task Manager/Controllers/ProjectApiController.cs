using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.ProjectViewModel;

namespace Task_Manager.Controllers
{
    public class ProjectApiController : ApiController
    {
        Log log = new Log();
        TaskContext db = new TaskContext();

        public String CreateProj(tempProj proji)
        {
            var session = HttpContext.Current.Session;
            string id = session["UserID"].ToString();
            session["project"] = null;
            Project projinst = new Project();

            projinst.id = proji.id;
            projinst.categroy = db.caterory.Find(proji.categoryId);
            projinst.Project_Name = proji.projectName;
            projinst.work_order = proji.workorder;
            projinst.Created_By = db.user.Find(Convert.ToInt32(id));
            projinst.Created_On = DateTime.Now;
            projinst.Start_Date = proji.startDate;
            projinst.End_Date = proji.endDate;
            projinst.Work = proji.natureOfWork;
            projinst.Enable = true;
            projinst.categroy = db.caterory.Find(proji.categoryId);
            projinst.customer = db.customer.Find(proji.cId);
            CustomerContactDetail cust = new CustomerContactDetail();
            cust.project_manager = proji.pm;
            cust.address = proji.address;
            cust.email = proji.email;
            cust.contact_number ="+92"+ proji.number;
            projinst.customerContactDetail = cust;
            projinst.projectManager = db.user.Find(proji.pmId);

            var pro = db.project.Find(projinst.id);
            if (pro != null)
            {
                var cusDet = db.customer_contact_detail.Find(pro.customerContactDetail.customerContactDetailId);
                var newUser = db.user.Single(c => c.id == projinst.projectManager.id);
                pro.categroy = projinst.categroy;
                db.Entry(pro).CurrentValues.SetValues(projinst);

                projinst.customerContactDetail.customerContactDetailId = cusDet.customerContactDetailId;
                db.Entry(cusDet).CurrentValues.SetValues(projinst.customerContactDetail);
                pro.projectManager = newUser;

                //edit user as project manager USMAN
                if (db.SaveChanges() > 0)
                {

                    log.Update("Project", pro.id, id);
                    return "Project updated Succesfully";
                }
                else
                {
                    return "Project Not updated";
                }
            }
            else
            {
                var sessionId = HttpContext.Current.Session;
                string aid = sessionId["UserID"].ToString();

                var createdUser = db.user.Find(Convert.ToInt32(aid));
                CustomerContactDetail custdetail = new CustomerContactDetail();
                custdetail.project_manager = projinst.customerContactDetail.project_manager;
                custdetail.contact_number = projinst.customerContactDetail.contact_number;
                custdetail.email = projinst.customerContactDetail.email;
                custdetail.address = projinst.customerContactDetail.address;
                db.customer_contact_detail.Add(custdetail);
                var custTemp = db.customer.Find(projinst.customer.customerId);
                Project proj = new Project();
                proj.Created_By = createdUser;
                proj.work_order = projinst.work_order;
                proj.projectManager = db.user.Where(p => p.id == projinst.projectManager.id).FirstOrDefault();
                proj.customer = custTemp;
                proj.Created_On = DateTime.Now;
                proj.Enable = true;
                proj.Project_Name = projinst.Project_Name;
                proj.Start_Date = projinst.Start_Date;
                proj.End_Date = projinst.End_Date;
                proj.categroy = projinst.categroy;
                proj.customer = db.customer.Find(projinst.customer.customerId);
                proj.customerContactDetail = custdetail;
                proj.categroy = projinst.categroy;
                proj.Work = projinst.Work;
                proj.status = db.status.Find(1);
                db.project.Add(proj);
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        log.Create("Project", proj.id, aid);
                        return "Done";
                    }
                    else
                    {
                        return "Not successfull call try again";
                    }
                }
                catch (Exception ex)
                {
                    db.customer_contact_detail.Remove(custdetail);
                    return ex.ToString();
                }
                //  return "Project Not Added Successfully";
                //delete the last added customer detail 

                return "Project Added Sucessfully";
            }
        }

        [Route("/api/ProjectApi/"), HttpPut]
        public int set(int id)
        {

            var session = HttpContext.Current.Session;
            session["project"] = id;
            return 0;
        }

        [Route("/api/ProjectApi/"), HttpGet]
        public Project projectall(int id)
        {
            var user = db.project.Where(p => p.id == id).FirstOrDefault();
            return user;
        }

        //Grid View Of Data
        [Route("/api/ProjectApi/"), HttpGet]
        public List<ProjectGrid> projall()
        {
            List<ProjectGrid> toReturn = new List<ProjectGrid>();
            var session = HttpContext.Current.Session;
            int uid = Convert.ToInt32(session["UserID"]); //userid
            string role_Id = session["role_Id"].ToString();
            string nature;
            List<Project> Proj = new List<Project>();

            if (role_Id == "1" || role_Id == "2")
            {
                Proj = db.project.Where(c => c.Enable == true).ToList();
            }

            else
            {
                var employee = db.user.Find(uid);
                var projectToShow = db.tagging.Where(c => c.tasks.IsTicket == false && c.tasks.enable == true).ToList();
                var projects = db.project.Where(c => c.projectManager.id == uid).ToList();

                foreach (var entity in projectToShow)
                {
                    var user = entity.users.Where(c => c.id == uid).FirstOrDefault();
                    if (user == null)
                    {
                        continue;
                    }
                    else
                    {
                        Proj.Add(entity.project);
                    }
                }
                Proj.AddRange(projects);
            }


            foreach (var entity in Proj)
            {

                var stat = db.status.ToList();
                ProjectGrid toAdd = new ProjectGrid();
                if (entity.Work == "0")
                {
                    nature = "Supply";
                }
                else if (entity.Work == "1")
                {
                    nature = "Installation";
                }
                else
                {
                    nature = "Supply & Installation";
                }
                // toAdd.id = entity.id;
                toAdd.projectName = entity.Project_Name;
                toAdd.createdOn = entity.Created_On.Date.ToShortDateString();
                toAdd.startDate = entity.Start_Date.Date.ToShortDateString();
                toAdd.endDate = entity.End_Date.Date.ToShortDateString();
                toAdd.workorder = entity.work_order;
                toAdd.address = entity.customerContactDetail.address;
                toAdd.customerManager = entity.customerContactDetail.project_manager;
                toAdd.projectManager = entity.projectManager.user_Name;
                toAdd.customerName = entity.customer.customer_name;
                toAdd.userName = entity.Created_By.user_Name;
                toAdd.Email = entity.customerContactDetail.email;
                toAdd.PhoneNumber = "+92" + entity.customerContactDetail.contact_number;
                toAdd.productCategory = entity.categroy.name;
                toAdd.natureofWork = nature;

                //add if else OrderedParallelQuery whatever way to get which one is selected

                if (entity.status.id == 1)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1' selected>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 2)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2' selected>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 3)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3' selected>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 4)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4' selected>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 5)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5' selected>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 6)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6' selected>OnHold</option><option value='7'>Completed</option></select>";
                else
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7' selected >Completed</option></select>";

                if (role_Id == "1")
                {
                    toAdd.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + entity.id + ")'/> <button  class='btn btn-danger  fa fa-times' onclick='deleteUser(" + entity.id + ")'/><button  class='btn btn-success fa fa-file-word-o' onclick=\"upload(" + entity.id + ")\"></button>";
                }
                else
                {
                    toAdd.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + entity.id + ")'/> <button  class='btn btn-success fa fa-file-word-o' onclick='upload(" + entity.id + ")'/>";
                }
                toReturn.Add(toAdd);
            }
            return toReturn;
        }


        [Route("/api/ProjectApi/"), HttpPost]
        public string delete(int id)
        {
            if (id == 0)
            {
                var session = HttpContext.Current.Session;
                int pid = Convert.ToInt32(session["project"]);
                session["customerForTask"] = db.project.Where(o => o.id == pid).Select(i => i.customer.customerId).FirstOrDefault(); 
          
                return "complete mission";
            }

            var proj = db.project.Find(id);

            int porjectID = proj.id;

            var tagging = db.tagging.Where(c => c.project.id == porjectID).ToList();
            foreach (var entity in tagging)
            {
                entity.tasks.enable = false;
            }
            proj.Enable = false;

            if (db.SaveChanges() > 0)
            {
                var session = HttpContext.Current.Session;
                string Deleteuserid = session["UserID"].ToString();
                Log log = new Log();
                log.Delete("Project", id, Deleteuserid);
                return "Project Deleted Success!! ";
            }
            else
            {
                return "Project Not Deleted";
            }

        }

        [HttpDelete]
        public void projectStatus(projectStatus status)
        {
            var project = db.project.Find(status.user);
            project.status = db.status.Find(status.value);
            db.SaveChanges();    
        }
        [HttpDelete]
        public List<ProjectGrid> projectStatus(int id)
        {
            List<ProjectGrid> toReturn = new List<ProjectGrid>();
            var session = HttpContext.Current.Session;
            int uid = Convert.ToInt32(session["UserID"]); //userid
            string role_Id = session["role_Id"].ToString();
            string nature;
            List<Project> Proj = new List<Project>();

            if (role_Id == "1" || role_Id == "2")
            {
                Proj = db.project.Where(c => c.Enable == true && c.status.id==id).ToList();
            }

            else
            {
                var employee = db.user.Find(uid);
                var projectToShow = db.tagging.Where(c => c.tasks.IsTicket == false && c.tasks.enable == true).ToList();
                var projects = db.project.Where(c => c.projectManager.id == uid).ToList();

                foreach (var entity in projectToShow)
                {
                    var user = entity.users.Where(c => c.id == uid).FirstOrDefault();
                    if (user == null)
                    {
                        continue;
                    }
                    else
                    {
                        Proj.Add(entity.project);
                    }
                }
                Proj.AddRange(projects);
            }


            foreach (var entity in Proj)
            {

                var stat = db.status.ToList();
                ProjectGrid toAdd = new ProjectGrid();
                if (entity.Work == "1")
                {
                    nature = "Supply";
                }
                else if (entity.Work == "2")
                {
                    nature = "Installation";
                }
                else
                {
                    nature = "Supply & Installation";
                }
                // toAdd.id = entity.id;
                toAdd.projectName = entity.Project_Name;
                toAdd.createdOn = entity.Created_On.Date.ToShortDateString();
                toAdd.startDate = entity.Start_Date.Date.ToShortDateString();
                toAdd.endDate = entity.End_Date.Date.ToShortDateString();
                toAdd.workorder = entity.work_order;
                toAdd.address = entity.customerContactDetail.address;
                toAdd.customerManager = entity.customerContactDetail.project_manager;
                toAdd.projectManager = entity.projectManager.user_Name;
                toAdd.customerName = entity.customer.customer_name;
                toAdd.userName = entity.Created_By.user_Name;
                toAdd.Email = entity.customerContactDetail.email;
                toAdd.PhoneNumber = "+92" + entity.customerContactDetail.contact_number;
                toAdd.productCategory = entity.categroy.name;
                toAdd.natureofWork = nature;

                //add if else OrderedParallelQuery whatever way to get which one is selected

                if (entity.status.id == 1)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1' selected>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 2)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2' selected>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 3)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3' selected>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 4)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4' selected>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 5)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5' selected>Final Payment </option><option value='6'>OnHold</option><option value='7'>Completed</option></select>";
                else if (entity.status.id == 6)
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6' selected>OnHold</option><option value='7'>Completed</option></select>";
                else
                    toAdd.status = @"<select  onchange='status(this.value," + entity.id + ")' id=" + entity.id + "><option value='1'>Unassigned</option><option value='2'>Advance Received</option><option value='3'>Equipment Delivered</option><option value='4'>InProgress</option><option value='5'>Final Payment </option><option value='6'>OnHold</option><option value='7' selected >Completed</option></select>";

                if (role_Id == "1")
                {
                    toAdd.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + entity.id + ")'/> <button  class='btn btn-danger  fa fa-times' onclick='deleteUser(" + entity.id + ")'/><button  class='btn btn-success fa fa-file-word-o' onclick=\"upload(" + entity.id + ")\"></button>";
                }
                else
                {
                    toAdd.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + entity.id + ")'/> <button  class='btn btn-success fa fa-file-word-o' onclick='upload(" + entity.id + ")'/>";
                }
                toReturn.Add(toAdd);
            }
            return toReturn;
        }

       


    }
}