using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Vendor
{
    public class VendorDetail
    {
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorPaymenttype { get; set; }
        public string vendorCity { get; set; }
        public string vendorMobile { get; set; }
        public string vendorEmail { get; set; }
        public string vendorWebsite { get; set; }
        public bool vendorActive { get; set; }
        public string vendorCreditlimit { get; set; }
        public int vendorDays { get; set; }
        public string vendorPrefereedcourier { get; set; }
        public string Note { get; set; }
        public string action { get; set; }
    }
}