using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class ProjectApiController : ApiController
    {

        TaskContext db = new TaskContext();

        public String CreateProj(tempProj proji)
        {
            var session = HttpContext.Current.Session;
            string id = session["UserID"].ToString();
            session["project"] = null;
            Project projinst = new Project();

            projinst.id = proji.id;
            projinst.Project_Name = proji.projectName;
            projinst.work_order = proji.workorder;
            projinst.Created_By = db.user.Find(Convert.ToInt32(id));
            projinst.Created_On = DateTime.Now;
            projinst.Start_Date = proji.startDate;
            projinst.End_Date = proji.endDate;
            projinst.Enable = true;
            projinst.customer = db.customer.Find(proji.cId);
            CustomerContactDetail cust = new CustomerContactDetail();
            cust.project_manager = proji.pm;
            cust.address = proji.address;
            cust.email = proji.email;
            cust.contact_number = proji.number;


            projinst.customerContactDetail = cust;
            projinst.projectManager = db.user.Find(proji.pmId);

            var pro = db.project.Find(projinst.id);
            if (pro != null)
            {
                var cusDet = db.customer_contact_detail.Find(pro.customerContactDetail.customerContactDetailId);
                var user = db.user.Find(pro.projectManager.id);
                var user1 = db.user.Find(projinst.projectManager.id);
                db.Entry(pro).CurrentValues.SetValues(projinst);

                  projinst.customerContactDetail.customerContactDetailId=  cusDet.customerContactDetailId;
                
                db.Entry(cusDet).CurrentValues.SetValues(projinst.customerContactDetail);
                
                
                db.Entry(user).CurrentValues.SetValues(user1);
                //edit user as project manager USMAN
                if (db.SaveChanges() > 0)
                {
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
               // custdetail.Created_By = createdUser;
                db.customer_contact_detail.Add(custdetail);
                //projinst.customerid = custdetail.customerContactDetailId;
                //  Customer cust = new Customer();
                var custTemp = db.customer.Find(projinst.customer.customerId);
                Project proj = new Project();
                //add user as project manager   USMAN
                proj.Created_By = createdUser;
                proj.work_order = projinst.work_order;
                proj.projectManager = db.user.Where(p => p.id == projinst.projectManager.id).FirstOrDefault();
                proj.customer = custTemp;
                proj.Created_On = DateTime.Now;
                proj.Enable = true;
                proj.Project_Name = projinst.Project_Name;
                proj.Start_Date = projinst.Start_Date;
                proj.End_Date = projinst.End_Date;
                proj.customer = db.customer.Find(projinst.customer.customerId);
                proj.customerContactDetail = custdetail;
                db.project.Add(proj);
                try
                {
                    if (db.SaveChanges() > 0)
                    {
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

        //[Route("/api/ProjectApi/"), HttpPut]
        //public Project get()
        //{
        //    var session = HttpContext.Current.Session;

        //    Project obj = new Project();
        //    if (session["project"] != null)
      
        //    {

        //        string str = session["project"].ToString();
        //        session["project"] = null;
        //        obj = db.project.Find(Convert.ToInt32(str));

        //        str = obj.customerContactDetail.contact_number;
        //        updateProject view = new updateProject();
        //        view.cId=obj.customer.customerId;
        //        view.pId=obj.id;
        //        view.sDate=obj.Start_Date.ToString();
        //        view.eDate=obj.End_Date.ToString();;
        //        view.pmId=obj.projectManager.id;
        //        view.workorder = obj.work_order;
        //        view.cManager=obj.customerContactDetail.project_manager;
        //        view.cContact=obj.customerContactDetail.contact_number;
        //        view.cEmail=obj.customerContactDetail.email;
        //        view.cAddress=obj.customerContactDetail.address;


        //        obj.customerContactDetail.contact_number = str.Substring(3);

        //        return obj;

        //    }

        //    else
        //    {
        //        obj.id = 0;
        //        return obj;
        //    }

        //}

        //to get a single user for filling form
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

            List<Project> Proj = new List<Project>();

            if (session["UserID"].ToString() == "5")
            {
                Proj = db.project.Where(c => c.Enable == true).ToList();
            }

            else
            {
                var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                Proj = db.project.Where(c => c.Enable == true && c.Created_By.id == createdBy.id).ToList();
            }


            foreach (var entity in Proj)
            {
                ProjectGrid toAdd = new ProjectGrid();
                toAdd.id = entity.id;
                toAdd.projectName = entity.Project_Name;
                toAdd.startDate = entity.Start_Date.Date;
                toAdd.endDate = entity.End_Date.Date;
                toAdd.workorder = entity.work_order;
                toAdd.address = entity.customerContactDetail.address;
                toAdd.customerManager = entity.customerContactDetail.project_manager;
                toAdd.projectManager = entity.projectManager.user_Name;
                toAdd.customerName = entity.customer.customer_name;
                toAdd.userName = entity.Created_By.user_Name;
                toAdd.Email = entity.customerContactDetail.email;
                toAdd.PhoneNumber = entity.customerContactDetail.contact_number;

                toReturn.Add(toAdd);
            }
            return toReturn;
        }


        [Route("/api/ProjectApi/"), HttpPost]
        public String delete(int id)
        {
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
                return "Project Deleted Success!! ";
            }
            else
            {
                return "Project Not Deleted";
            }

        }
    }
}