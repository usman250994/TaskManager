using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class responseListForMessage
    {
        public List<responseMessage> responseList { get; set; }
        public string taggedUsers{get;set;}
        public string task_no { get; set; }
        public string task_name { get; set; }
        public string projectname { get; set; }
        public string Description { get; set; }
        public string sDate { get; set; }
        public string eDate { get; set; }
        public bool Email { get; set; }
        public bool SMS { get; set; }
        public string customername { get; set; }
        public string customercode { get; set; }
        public string assigned { get; set; }
        public string createdby { get; set; }
        public string closingNote { get; set; }
        public string completeNote { get; set; }
        public string closingDate { get; set; }
        public string completeDate { get; set; }
        public string closingUser { get; set; }
        public string completingUser { get; set; }
        public string completedocument { get; set; }
        public string closingdocument { get; set; }
    }
}