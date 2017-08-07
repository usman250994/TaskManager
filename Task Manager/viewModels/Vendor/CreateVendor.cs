using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.Vendor
{
    public class CreateVendor
    {


        public CreateVendor()
        
        {
            vendorsystemCategory = new List<dropCust>();


        }
        public int vendorid { get; set; }
        public string vendorName { get; set; }
        public int payType { get; set; }
        public string vendoraddress { get; set; }
        public string vendorcity { get; set; }
        public string vendormobNo { get; set; }
        public string vendorteleNo { get; set; }
        public string vendorEmail { get; set; }
        public string vendorwebsite { get; set; }
        public string vendorfax { get; set; }
        public string vendorNote { get; set; }
        public bool isApprove { get; set; }
        public bool isActive { get; set; }
        public string vendorcreditLimit { get; set; }
        public int vendordays { get; set; }
        public string vendorpreferredCourier { get; set; }
        public List<dropCust> vendorsystemCategory { get; set; }
        public string createdBy { get; set; }
        public String user_name { get; set; }
        public string userregNo { get; set; }
        public string usernic { get; set; }
        public string usertitle { get; set; }
        public string useremail { get; set; }
        public string usermobNo { get; set; }
        public string usertelephoneNo { get; set; }
        public string userfax { get; set; }
    }
}