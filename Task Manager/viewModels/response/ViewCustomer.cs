using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.response
{
    public class ViewCustomer
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public string action { get; set; }
    }
}