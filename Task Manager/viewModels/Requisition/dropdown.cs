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
        public string serialNo { get; set; }
    }
}