using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Customer
    {
        public int customerId { get; set; }
        public string customer_name { get; set; }
        public string address { get; set; }
        public string Phonenumber { get; set;}
        public string Website { get; set; }
        public string Email { get; set; }
        public virtual Users Created_By { get; set; }
        public bool enable { get; set; }
    }
}