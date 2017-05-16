using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class TicketInitialrespone
    {
        public string customerId { get; set; }        

        public List<projectDropDownInTickets> projects { get; set; }
    }
}