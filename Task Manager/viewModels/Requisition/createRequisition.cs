using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Requisition
{
    public class createRequisition
    {
        public string sno { get; set; }
        public int customerid { get; set; }
        public int projectid { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string units { get; set; }
        public string quantity { get; set; }
        public DateTime date { get; set; }
    }
}