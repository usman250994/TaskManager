using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class tempProj
    {
        public int id { get; set; }
        public string projectName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int pmId { get; set; }
        public string pm { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public int cId { get; set; }
    }
}