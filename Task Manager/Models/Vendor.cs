using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Vendor
    {
        public int id { get; set; }
        public string vendorName { get; set; }
        public virtual VendorType type { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string mobNo { get; set; }
        public string teleNo { get; set; }
        public string Email { get; set; }
        public string website { get; set; }
        public string fax { get; set; }
        public bool isApprove { get; set; }
        public bool isActive { get; set; }
        public string creditLimit { get; set; }
        public int days { get; set; }
        public string preferredCourier { get; set; }
        public string syestemCategory { get; set; }
        public string note { get; set; }
        public bool enable { get; set; }
        public virtual Users createdBy { get; set; }
        public virtual VendorContact vendorContact { get; set; }
    }
}