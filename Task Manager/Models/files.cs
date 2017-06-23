using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Files
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileCode { get; set; }
        public String filetype { get; set; }
        public DateTime createdOn { get; set; }
      
    }
}