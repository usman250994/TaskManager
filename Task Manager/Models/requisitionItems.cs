using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class requisitionItems
    {
        public int id { get; set; }
       
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string units { get; set; }
        public string quantity { get; set; }
        public string issuedQuanatity { get; set; }
        public DateTime requiredDate { get; set; }
        public virtual Requisition requisition { get; set; }
        public bool enable { get; set; }
    }
}