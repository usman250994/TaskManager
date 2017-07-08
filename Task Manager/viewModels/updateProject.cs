using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class updateProject
    {
        public int cId { get; set; }
        public int pId { get; set; }
        public int pmId { get; set; }
        public string sDate { get; set; }
        public string pName { get; set; }
        public string eDate { get; set; }
        public string cManager { get; set; }
        public int workorder { get; set; }
        public string cContact { get; set; }
        public string cEmail { get; set; }
        public string cAddress { get; set; }
        public int categoryID { get; set; }
        public List<tagUsersView> userList { get; set; }
        public List<dropCust> dropList { get; set; }
        public List<Category> categoryList { get; set; }
    }
}