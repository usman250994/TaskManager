using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Requisition
    {
        public int id { get; set; }
        public string Sno { get; set; }
        public virtual Customer customer { get; set; }
        public virtual Project project { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string units { get; set; }
        public string requiredQuantity { get; set; }
        public string issuedQuanatity { get; set; }
        public DateTime requiredDate { get; set; }
        public virtual Users createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public virtual Users approvedBy { get; set; }
        public DateTime approvedDate { get; set; }
        public virtual Users issuedBy { get; set; }
        public virtual Users issuedTo { get; set; }
        public DateTime issuedDate { get; set; }
        public bool enable { get; set; }
    }
}