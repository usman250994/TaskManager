using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class VendorType
    {
        public int id { get; set; }
        public string type_Name { get; set; }
        public bool enable { get; set; }
    }
}