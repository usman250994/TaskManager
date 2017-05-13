using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.viewModels.response;

namespace Task_Manager.Models
{
    public class updateTaskReturning
    {
        public TaskDropdown dropdowns { get; set; }
        public Task task { get; set; }
        public int projectId { get; set; }
        public  List<int> tags { get; set;}
    }
}