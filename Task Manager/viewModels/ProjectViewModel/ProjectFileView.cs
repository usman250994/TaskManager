﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.viewModels.Project;

namespace Task_Manager.viewModels
{
    public class ProjectFileView
    {
        public List<taskTab> tasktab { get; set; }
        public List<FilesTab> filetab { get; set; }
    }
}