using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class requisitionItem
    {
        public int id { get; set; }
        public string itemCode { get; set; }
        public string quantity { get; set; }
        public string itemName { get; set; }

    }
}