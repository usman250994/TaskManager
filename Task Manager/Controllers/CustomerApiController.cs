using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.response;

namespace Task_Manager.Controllers
{
    public class CustomerApiController : ApiController
    {
        TaskContext db = new TaskContext();

        //To Add User In Database Table Customer
        [HttpPost]
        public String CreateCustomer(Customer cust)
        {
            if (cust == null)
            {
                return "Please Insert Customer";
            }
            else
            {
                var client = db.customer.Find(cust.customerId);
             
                if (client != null)
                {
                    var sessionId = HttpContext.Current.Session;
                    string id = sessionId["UserID"].ToString();
                    var createdUser = db.user.Find(Convert.ToInt32(id));
                    cust.Created_By = createdUser;
                    cust.city_code = cust.city_code + client.city_code.Substring(2, 4)+cust.OnBoarddate.Year;
                    cust.createdOn = DateTime.Now;
                    db.Entry(client).CurrentValues.SetValues(cust);
                    
                    
                    db.SaveChanges();
                }
                else
                {
                    var sessionId = HttpContext.Current.Session;
                    string id = sessionId["UserID"].ToString();
                    var createdUser = db.user.Find(Convert.ToInt32(id));
                    cust.Created_By = createdUser;
                    cust.createdOn = DateTime.Now;
                    //
                    int a = db.customer.Count() + 1;
                    var customerNumber = a.ToString().PadLeft(4, '0');
                    var customerYear = cust.OnBoarddate.Year;
                    cust.city_code = cust.city_code + customerNumber + customerYear;

                    //
                    db.customer.Add(cust);
                    if (db.SaveChanges() == 1)
                    {
                        return "Customer Added";
                    }
                    else
                        return "Customer Not Added";
                }
                return "Customer Update Sucessfully";
            }
        }

        //To Get All The Customer List (To Show in Grid View Of User)
        [HttpGet]
        public List<ViewCustomer> custall()
        {
            List<Customer> list = new List<Customer>();
            List<ViewCustomer> toReturn = new List<ViewCustomer>();
            var session = HttpContext.Current.Session;
            if (session["UserID"].ToString() == "5")
            {
                list = db.customer.Where(d => d.enable == true).ToList();
                for (int i = 0; list.Count > i; i++)
                {
                    ViewCustomer viewcustomer = new ViewCustomer();
                    viewcustomer.id = list[i].customerId;
                    viewcustomer.code = list[i].city_code;
                    viewcustomer.name = list[i].customer_name;
                    viewcustomer.address = list[i].address;
                    viewcustomer.contact = list[i].Phonenumber;
                    viewcustomer.email = list[i].Email;
                    viewcustomer.website = list[i].Website;
                    viewcustomer.onboardDate = list[i].OnBoarddate.Date.ToShortDateString();
                    viewcustomer.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + list[i].customerId + ")'/> <button  class='btn btn-danger  fa fa-times' onclick='deleteUser(" + list[i].customerId + ")'/>";
                    toReturn.Add(viewcustomer);
                }
            }
            else
            {
                var createdBy = db.user.Find(Convert.ToInt32(session["UserID"]));
                list = db.customer.Where(d => d.enable == true && d.Created_By.id == createdBy.id).ToList();
                for (int i = 0; list.Count > i; i++)
                {
                    ViewCustomer viewcustomer = new ViewCustomer();
                    viewcustomer.code = list[i].city_code;
                    viewcustomer.name = list[i].customer_name;
                    viewcustomer.address = list[i].address;
                    viewcustomer.contact = list[i].Phonenumber;
                    viewcustomer.email = list[i].Email;
                    viewcustomer.website = list[i].Website;
                    viewcustomer.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + list[i].customerId + ")'/> <button  class='btn btn-danger  fa fa-times' onclick='deleteUser(" + list[i].customerId + ")'/>";
                    toReturn.Add(viewcustomer);
                }
            }

            return toReturn;
        }

        //Delete user function
        [HttpPost]
        public String delete(int id)
        {

            List<int> list = new List<int>();

            list = db.project.Where(d => d.customer.customerId == id).Select(d => d.id).ToList();

            // [Route("/api/ProjectApi/"), HttpPost]


            for (int i = 0; i < list.Count; i++)
            {
                var result = new ProjectApiController().delete(list[i]);

            }
            db.customer.Find(id).enable = false;

            //also remove complete hierarchy
            if (db.SaveChanges() == 1)
            {
                return "Customer Delete Success ";
            }
            else
            {
                return "Customer Not Deleted";
            }

        }

        // For Update 
        [HttpPut]
        public int set(int id)
        {
            var session = HttpContext.Current.Session;
            session["Customer"] = id;
            return 0;
        }

        [Route("/api/CustomerApi/"), HttpPut]
        public customerdropdown get()
        {
            var session = HttpContext.Current.Session;
            customerdropdown custdropdown = new customerdropdown();
            Customer cust = new Customer();
            if (session["Customer"] != null)
            {
                string str = session["Customer"].ToString();
                session["Customer"] = null;
                cust = db.customer.Find(Convert.ToInt32(str));
                custdropdown.cust_id = cust.customerId;
                custdropdown.username = cust.customer_name;
                custdropdown.citycode = cust.city_code.Substring(0,2);
                custdropdown.address = cust.address;
                custdropdown.contact = cust.Phonenumber;
                custdropdown.email = cust.Email;
                custdropdown.website = cust.Website;
                custdropdown.onboardDate = cust.OnBoarddate;
                List<City> citylist = new List<City>();
                citylist = db.city.ToList();
                custdropdown.city = citylist;
                return custdropdown;
            }
            else
            {
                List<City> citylist = new List<City>();
                citylist = db.city.ToList();
                custdropdown.city = citylist;
                return custdropdown;
            }
        }
        //to get a single user for filling form
        [Route("/api/CustomerApi/"), HttpGet]
        public Users Userall(int id)
        {
            var user = db.user.Where(p => p.id == id).FirstOrDefault();
            return user;
        }
    }
}