using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Requisition
{
    public class dropdown
    {
        public List<dropCust> customer { get; set; }
        public List<dropProd> project { get; set; }
        public List<dropCust> units { get; set; }
        public string serialNo { get; set; }
        public bool formReq { get; set; }
        public string projectname { get; set; }
        public string customername { get; set; }
    }
}