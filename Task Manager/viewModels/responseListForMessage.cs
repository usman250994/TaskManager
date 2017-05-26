using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class responseListForMessage
    {
        public List<responseMessage> responseList { get; set; }
        public Task myTask { get; set; }
        public List<string> taggedUsers{get;set;}
       
    }
}