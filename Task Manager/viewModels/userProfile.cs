using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class userProfile
    {
        public string oldPass { get; set; }
        public string newPass { get; set; }
        public string rePass { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string number { get; set; }
    }
}