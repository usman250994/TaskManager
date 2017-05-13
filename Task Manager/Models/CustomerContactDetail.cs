using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class CustomerContactDetail
    {
        public int customerContactDetailId { get; set; }
        public string project_manager { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public virtual Users Created_By { get; set; }
    }
}