using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.response
{
    public class taskResponse
    {
        public int ticket_name { get; set; }
        public string task_name { get; set; }
        public string cName { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool sms { get; set; }
        public bool email { get; set; }
        public string users { get; set; }
        public string cCode { get; set; }
        public string status { get; set; }
        public string button { get; set; }
        public string bran_Code { get; set; }

    }
}