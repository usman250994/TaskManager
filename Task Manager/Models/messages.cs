using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class messages
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string message { get; set; }
        public virtual Users sentBy { get; set; }
        public DateTime timeStamp { get; set; }
    }
}