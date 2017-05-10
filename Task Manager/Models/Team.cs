using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Team
    {
        public Team()
        {
            team_member = new List<Users>();
        }
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public virtual Users team_lead { get; set; }
        public string team_name { get; set; }
        public virtual List<Users> team_member { get; set; }
    }
}