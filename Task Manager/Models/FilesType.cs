using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class FilesType
    {
        public int id { get; set; }
        public string  Filecode { get; set; }
        public string Filename { get; set; }
        public bool enable { get; set; }
    }
}