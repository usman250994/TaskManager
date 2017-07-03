using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class tempTask
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string task_name { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime created_on { get; set; }
        public DateTime modified { get; set; }
        public string ticketCode { get; set; }
        public bool sms { get; set; }
        public bool email { get; set; }
        public int status { get; set; }
        public List<tagUsersView> tempUsers { get; set; }
    }
}