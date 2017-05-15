using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Task
        {
        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string task_name { get; set; }
        public string description { get; set; }
        public DateTime created_on { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool sms { get; set; }
        public bool email { get; set; }
        public int status { get; set; }
        public bool enable { get; set; }
        public virtual Users Created_By { get; set; }
        public virtual ticketContact customer { get; set; }

    }
}