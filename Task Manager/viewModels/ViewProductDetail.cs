using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class ViewProductDetail
    {
        public string name { get; set; }
        public string type { get; set; }
        public string Catogrey { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string barcode { get; set; }
        public DateTime inward_date { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string vendor_name { get; set; }
        public string img_path { get; set; }
    }
}