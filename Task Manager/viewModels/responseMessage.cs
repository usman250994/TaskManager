using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class responseMessage
    {

        public DateTime timeStamp { get; set; }
        public string name { get; set; }
        public string initials { get; set; }
        public bool direction { get; set; }
        public string location { get; set; }
        public string message { get; set; }
     
    }
}