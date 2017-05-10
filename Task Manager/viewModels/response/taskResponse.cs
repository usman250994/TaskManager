using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.response
{
    public class taskResponse
    {
        public Task task { get;set; }
        public List<String> users { get; set; }
    }
}