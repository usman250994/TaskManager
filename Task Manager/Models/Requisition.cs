using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Requisition
    {
       
        public int id { get; set; }
        public int serialNo { get; set; }
        public virtual Customer customer { get; set; }
        public virtual Project project { get; set; }
        public virtual Users createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public virtual Users approvedBy { get; set; }
        public DateTime approvedDate { get; set; }
        public virtual Users issuedBy { get; set; }
        public virtual Users receivedBy { get; set; }
        public DateTime issued_received_Date { get; set; }
        public string approvalNote { get; set; }
        public bool enable { get; set; }



    }
}