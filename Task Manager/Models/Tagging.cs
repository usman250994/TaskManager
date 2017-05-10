using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Tagging
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public virtual Project project { get; set; }
        public virtual Task tasks { get; set; }
        public virtual List<Team> teams { get; set; }
        public virtual List<Users> users { get; set; }
    }
}