﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Project
{
    public class taskTab
    {
        public int task_No { get; set; }
        public string task_Name { get; set; }
        public DateTime start_Date { get; set; }
        public DateTime end_Date { get; set; }
    }
}