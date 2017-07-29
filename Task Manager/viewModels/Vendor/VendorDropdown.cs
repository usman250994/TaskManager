using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.Vendor
{
    public class VendorDropdown
    {
        public List<dropCust> systemCategory { get; set; }
        public List<VendorType> type { get; set; }
    }
}