using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class ticketContact
    {

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public bool enable { get; set; }
        public List<Task> _tickets { get; set; }
        public ticketContact() 
        {
            _tickets = new List<Task>();
        }
    }
}