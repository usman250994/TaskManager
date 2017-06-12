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
    public class projectupdateController : ApiController
    {


        TaskContext db = new TaskContext();

        [HttpGet]
        public updateProject updateModel()
        {
            Project obj = new Project();
            updateProject view = new updateProject();
            var session = HttpContext.Current.Session;
            if (session["project"] != null)
            {

                string str = session["project"].ToString();
                session["project"] = null;
                obj = db.project.Find(Convert.ToInt32(str));

                str = obj.customerContactDetail.contact_number;

                view.cId = obj.customer.customerId;
                view.pId = obj.id;
                view.pName = obj.Project_Name;
                view.sDate = obj.Start_Date.ToString();
                view.eDate = obj.End_Date.ToString(); ;
                view.pmId = obj.projectManager.id;
                view.cManager = obj.customerContactDetail.project_manager;
                view.cContact = obj.customerContactDetail.contact_number;
                view.cEmail = obj.customerContactDetail.email;
                view.cAddress = obj.customerContactDetail.address;


                obj.customerContactDetail.contact_number = str.Substring(3);


                List<tagUsersView> list = new List<tagUsersView>();

                list = db.user.Where(o => o.Enable == true).Select(o => new tagUsersView { id = o.id, name = o.user_Name }).ToList();
             
                view.userList=list;

                 List<dropCust> dropcustomer = new List<dropCust>();

            dropcustomer = db.customer.Where(o => o.enable == true).Select(o => new dropCust { name = o.customer_name, id = o.customerId }).ToList();


                view.dropList= dropcustomer;
            }

            return view;
        }
    }
}
