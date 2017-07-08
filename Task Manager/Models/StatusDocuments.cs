using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class StatusDocuments
    {
        public int id { get; set; }
        public string documentPath { get; set; }
        public DateTime createdOn { get; set; }
        public virtual Users createdBy { get; set; }
        public bool isClose { get; set; }
       
    }
}