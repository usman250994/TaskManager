﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class CustomerApiController : ApiController
    {
        TaskContext db = new TaskContext();

        //To Add User In Database Table Customer
        [Route("/api/CustomerApi/"), HttpPost]
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
                    db.Entry(client).CurrentValues.SetValues(cust);
                    db.SaveChanges();
                }
                else
                {
                    
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
        [Route("/api/CustomerApi/"), HttpGet]
        public List<Customer> custall()
        {
            List<Customer> list = new List<Customer>();
            list = db.customer.Where(d => d.enable == true).ToList();
            return list;
        }



        //Delete user function
        [Route("/api/CustomerApi/"), HttpPost]
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
        [Route("/api/CustomerApi/"), HttpPut]
        public int set(int id)
        {
            var session = HttpContext.Current.Session;
            session["Customer"] = id;
            return 0;
        }

        [Route("/api/CustomerApi/"), HttpPut]
        public Customer get()
        {
            var session = HttpContext.Current.Session;
            Customer cust = new Customer();
            if (session["Customer"] != null)
            {
                string str = session["Customer"].ToString();
                session.Clear();

                cust = db.customer.Find(Convert.ToInt32(str));
                return cust;
            }
            else
            {
                return cust;
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
