using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class files
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public int fileCode { get; set; }
        public virtual Users createdBy { get; set; }
        public DateTime createdOn { get; set; } 
    }
}