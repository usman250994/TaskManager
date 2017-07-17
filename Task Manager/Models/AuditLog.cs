using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class AuditLog
    {
        public int id { get; set; }
        public string actionCode { get; set; }
        public int activityId { get; set; }
        public string tableName { get; set; }
        public DateTime activityDate { get; set; }
        public string createdBy { get; set; }
    }
}