using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Requisition
{
    public class viewRequisition
    {
        public int id { get; set; }
        public int serialNo { get; set; }
        public string customerName { get; set; }
        public string projectName { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string units { get; set; }
        public string requiredQuantity { get; set; }
        public string requiredDate { get; set; }
        public string issuedQuantity { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string approvedBy { get; set; }
        public string approvedDate { get; set; }
        public string issuedBy { get; set; }
        public string issuedTo { get; set; }
        public string issuedDate { get; set; }
        public string  action { get; set; }
    }
}