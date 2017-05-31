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
        public string users { get; set; }
        public string notify{get;set;}
    }
}