using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Requisition
{
    public class createRequisition
    {
        public int serialNo { get; set; }
        public int customerId { get; set; }
        public int projectId { get; set; }
        public bool formReq { get; set; }
        public virtual List<reqItems> items { get; set; }

        public class reqItems
        {
            public string itemName { get; set; }
            public string itemCode { get; set; }
            public dropCust units { get; set; }
            public string quantity { get; set; }
            public string issueQuant { get; set; }
            public DateTime dateReq { get; set; }
        }
        
        
    }
     
}