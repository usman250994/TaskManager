using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Project
    {
        public int id { get; set; }
        public string Project_Name { get; set; }
        public virtual Users Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }   
        public virtual Customer customer { get; set; }
        public virtual CustomerContactDetail customerContactDetail { get; set; }
        public virtual Users projectManager { get; set; }
        public bool Enable { get; set; }     
        
    }
}