using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class responseInventory
    {

        public string name { get; set; }
        public string type { get; set; }
        public string catogrey { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string barcode { get; set; }
        public string vendorName { get; set; }
        public string action { get; set;}
        public DateTime inward { get; set; }
    }
}