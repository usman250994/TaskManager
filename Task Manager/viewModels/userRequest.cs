using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class userRequest
    {
        public int id { get; set; }
        public string user_Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime Createdon { get; set; }
        public bool Enable { get; set; }
        public int Created_By { get; set; }
        public int Roles_id { get; set; }
    }
}