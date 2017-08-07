using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Task_Manager.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext()
          //  : base("Data Source=192.168.1.177; Initial Catalog=Task Manager; User Id=sa; Password=resco@1234; Integrated Security=False") // For Server
        : base("Data Source=.; Initial Catalog=Task Manager; User Id=sa; Password=resco123!; Integrated Security=False") // For LocalDB
        { }

        public DbSet<Roles> roles { get; set; }
        public DbSet<Project> project { get; set; }
        public DbSet<Requests> request { get; set; }
        public DbSet<Tagging> tagging { get; set; }
        public DbSet<Users> user { get; set; }
        public DbSet<Task> task { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<ticketContact> tickets { get; set; }
        public DbSet<CustomerContactDetail> customer_contact_detail { get; set; }
        public DbSet<messages> messages { get; set; }
        public DbSet<Category> caterory { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<City> city { get; set; }
        public DbSet<StatusDocuments> ticketdocuments { get; set; }
        public DbSet<Files> files { get; set; }
        public DbSet<FilesType> filetype { get; set; }
        public DbSet<AuditLog> auditLog { get; set; }
        public DbSet<Vendor> vendor { get; set; }
        public DbSet<VendorContact> vendorContact { get; set; }
        public DbSet<VendorType> vendorType { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<Requisition> requisition { get; set; }
    }


}