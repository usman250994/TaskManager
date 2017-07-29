using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Category
    {

        public Category()
        {
            vendors = new HashSet<Vendor>();
        }
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public bool enable { get; set; }
        public virtual ICollection<Vendor> vendors { get; set; }
    }
}