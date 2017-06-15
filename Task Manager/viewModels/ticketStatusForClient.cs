using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class ticketStatusForClient
    {

        public List<clientsTicketRespone> list { get; set; }
        public string code { get; set; }
        public string customer_name { get; set; }
    }
}