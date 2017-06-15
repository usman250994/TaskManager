using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class internalTickets
    {
        public int id { get; set; }
        public string uname { get; set; }
        public int projectId { get; set; }
        public string issue { get; set; }
        public string branchCode { get; set; }
        public DateTime sdate { get; set; }
        public DateTime edate { get; set; }
        public DateTime created_on { get; set; }
        public DateTime Modified { get; set; }
        public bool email { get; set; }
        public bool sms { get; set; }
        public List<tagUsersView> assignedTo { get; set; }

    }
}