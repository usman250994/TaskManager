using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class clientsTicketRespone
    {
        public int tid { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string branch_code { get; set; }
        public string project_name { get; set; }
        public DateTime date { get; set; }
    }
}