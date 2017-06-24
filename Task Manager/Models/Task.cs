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

        public Task()
        {
            discussion = new List<messages>();
        }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ticket_code { get; set; }
        public string task_name { get; set; }
        public string description { get; set; }
        public string branch_code { get; set; }
        public DateTime created_on { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime LastModify { get; set; }
        public bool sms { get; set; }
        public bool email { get; set; }
        public int status { get; set; }
        public bool enable { get; set; }
        public virtual Users Created_By { get; set; }
        public bool IsTicket { get; set; }
        public List<messages> discussion { get; set; }
    }
}