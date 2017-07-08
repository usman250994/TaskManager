using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class ProjectGrid
    {
        public int id { get; set; }
        public string projectName { get; set; }
        public string createdBy { get; set; }
        public string createdOn { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string userName { get; set; }
        public string address { get; set; }
        public string customerName { get; set; }
        public int workorder { get; set; }
        public string projectManager { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string customerManager { get; set; }
        public string productCategory { get; set; }
        public string action { get; set; }
    }
}