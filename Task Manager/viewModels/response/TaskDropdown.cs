using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.response
{
    public class TaskDropdown
    {
        public List<Users> Users { get; set; }
        public List<Task_Manager.Models.Project> projects { get; set; }
        public List<Customer> customer { get; set; }
        public Task task { get; set; }
    }
}