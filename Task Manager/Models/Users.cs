using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Users
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string user_Name { get; set; }
        public string Password { get; set; }
        public string Address  { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime  Createdon { get; set; }
        public bool Enable { get; set; }
        public virtual List<Tagging> users { get; set; }
        public virtual Users Created_By { get; set; }
    }
}