﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.viewModels
{
    public class updateOrCreateDropDown
    {

        public List<Customer> cust { get;set; }
        public Task_Manager.Models.Project proj { get; set; }
        public List<Users> candidsForProjManager { get; set; }


    }
}