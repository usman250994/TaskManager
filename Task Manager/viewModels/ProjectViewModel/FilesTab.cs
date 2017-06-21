using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels.Project
{
    public class FilesTab
    {
        public string file_Name { get; set; }
        public string file_Type { get; set; }
        public string fileCode { get; set; }
        public DateTime uploaded_date { get; set; } 
    }
}