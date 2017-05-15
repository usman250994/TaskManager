using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class TicketRequest
    {
        public string username { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public int customerid { get; set; }
        public int projectid { get; set; }
        public string desc { get; set; }
    }
}