using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class taskupdate
    {
        public string taskname { get; set; }
        public int cid { get; set; }
        public int pid { get; set; }
        public string issue { get; set; }
        public string sdate { get; set; }
        public string edate { get; set; }
        public List<tagUsersView> tags { get; set; }
        public bool sms { get; set; }
        public bool email { get; set; }
    }
}