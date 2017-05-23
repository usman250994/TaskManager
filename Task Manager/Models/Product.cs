using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class Product
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string product_name { get; set; }
        public bool islocal { get; set; }
        public virtual Category category { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string barcode { get; set; }
        public string vendor_name { get; set; }
        public virtual Users user { get; set; }
        public bool enable { get; set; }
    }
}