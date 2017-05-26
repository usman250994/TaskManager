using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class customerdropdown
    {
        public Customer customers { get; set; }
        public List<City> city { get; set; }
    }
}