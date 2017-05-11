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
        public String CreateProj(Project projinst)
        {
            var pro = db.project.Find(projinst.id);
            if (pro != null)
            {
                var cusDet = db.customer_contact_detail.Find(pro.customerContactDetail.customerContactDetailId);
                var user = db.user.Find(pro.projectManager.id);

                var user1 = db.user.Find(projinst.projectManager.id);


                db.Entry(pro).CurrentValues.SetValues(projinst);
                db.Entry(cusDet).CurrentValues.SetValues(projinst.customerContactDetail);
                db.Entry(user).CurrentValues.SetValues(user1);
                //edit user as project manager USMAN
                if (db.SaveChanges()>0)
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
                CustomerContactDetail custdetail = new CustomerContactDetail();
                custdetail.project_manager = projinst.customerContactDetail.project_manager;
                custdetail.contact_number = projinst.customerContactDetail.contact_number;
                custdetail.email = projinst.customerContactDetail.email;
                custdetail.address = projinst.customerContactDetail.address;
                db.customer_contact_detail.Add(custdetail);
                //projinst.customerid = custdetail.customerContactDetailId;
              //  Customer cust = new Customer();
                var custTemp = db.customer.Find(projinst.customer.customerId);
                Project proj = new Project();
               //add user as project manager   USMAN

                proj.projectManager = db.user.Where(p => p.id == projinst.projectManager.id).FirstOrDefault();
                proj.customer = custTemp;
                proj.Created_On = DateTime.Now;
                proj.Created_By = db.user.Find(1);
                proj.Enable = true;
                proj.Project_Name = projinst.Project_Name;
                proj.Start_Date = projinst.Start_Date;
                proj.End_Date = projinst.End_Date;
                proj.customer = db.customer.Find(projinst.customer.customerId);
                proj.customerContactDetail = custdetail;
                db.project.Add(proj);
                try
                {
                    if (db.SaveChanges() >0 )
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

        [Route("/api/ProjectApi/"), HttpPut]
        public updateOrCreateDropDown get()
        {
            var session = HttpContext.Current.Session;

            updateOrCreateDropDown obj = new updateOrCreateDropDown();
            if (session["project"] != null)
            {
                string str = session["project"].ToString();
                session.Clear();
                var proj = db.project.Find(Convert.ToInt32(str));
                obj.proj = proj;
                List<Customer> cust = new List<Customer>();
                cust = db.customer.Where(d=>d.enable==true).ToList();
                obj.cust = cust;

                //getting list of users as project manager

                obj.candidsForProjManager = db.user.Where(p => p.Enable == true).ToList();
                return obj;
            }
            else
            {
                List<Customer> cust = new List<Customer>();
                cust = db.customer.Where(d=>d.enable==true).ToList();
                obj.cust = cust;

                //getting list of users as project manager
                obj.candidsForProjManager = db.user.Where(p => p.Enable == true).ToList();
                return obj;
            }

        }

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
            var Proj = db.project.Where(c => c.Enable == true).ToList();
            foreach (var entity in Proj)
            {
                ProjectGrid toAdd = new ProjectGrid();
                toAdd.id = entity.id;
                toAdd.projectName = entity.Project_Name;
                toAdd.startDate = entity.Start_Date.Date;
                toAdd.endDate = entity.End_Date.Date;
                toAdd.customerManager= entity.customerContactDetail.project_manager;
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