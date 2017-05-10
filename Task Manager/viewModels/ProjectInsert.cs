using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class ProjectInsert
    {
        public int proj_id { get; set; }
        public int  customerid { get; set; }
        public string Project_Name { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string project_manager { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public string Address { get; set; }

    }
}