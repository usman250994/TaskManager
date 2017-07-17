using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class Activity
    {
        public string actionName { get; set; }
        public string tableName { get; set; }
        public string Perform { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
    }
}