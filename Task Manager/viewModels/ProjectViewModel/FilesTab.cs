using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Project
{
    public class FilesTab
    {
        public string file_Name { get; set; }
        public int file_Type { get; set; }
        public string uploaded_by { get; set; }
        public string uploaded_date { get; set; } 
    }
}