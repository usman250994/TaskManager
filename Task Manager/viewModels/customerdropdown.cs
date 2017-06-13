using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class customerdropdown
    {
        public int cust_id { get; set; }
        public string username { get; set; }
        public string citycode { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public List<City> city { get; set; }
    }
}