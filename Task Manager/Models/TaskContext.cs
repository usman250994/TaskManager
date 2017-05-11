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
            : base("Data Source=resco; Initial Catalog=Task Manager2; User Id=sa; Password=resco123!; Integrated Security=False")
        { }
      
        public DbSet<Roles> roles { get; set; }
        public DbSet<Project> project { get; set; }
        public DbSet<Requests> request { get; set; }
        public DbSet<Tagging> tagging { get; set; }
        public DbSet<Users> user { get; set; }
        public DbSet<Task> task { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<CustomerContactDetail> customer_contact_detail { get; set; }
    }
}