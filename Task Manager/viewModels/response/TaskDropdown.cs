﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels.response
{
    public class TaskDropdown
    {
        public List<Users> Users { get; set; }
        public List<Project> projects { get; set; }
        public Task task { get; set; }
    }
}