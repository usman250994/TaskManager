namespace Task_Manager.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task_Manager.Models.TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task_Manager.Models.TaskContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //seeding roles table
            List<Models.Roles> roles = new List<Models.Roles>();
            if (!context.roles.Any())
            {
                Task_Manager.Models.Roles role = new Models.Roles();
                role.enable = false;
                role.role_name = "Sudo";
                roles.Add(role);
                role.enable = true;
                role.role_name = "Admin";
                roles.Add(role);
                role.role_name = "Normal";
                roles.Add(role);
                role.role_name = "CSR";
                roles.Add(role);
                context.roles.AddRange(roles);
                context.SaveChanges();
            }
            // Seeding Users Table
            List<Models.Users> users = new List<Models.Users>();
            if (!context.user.Any())

            {
                Models.Users user = new Models.Users();
                user.Enable = false;
                user.Createdon = DateTime.Now;
                user.Roles_id = context.roles.Find(1);
                user.user_Name = "sudo";
                user.Password = "sudo";

                context.user.Add(user);
                context.SaveChanges();

                user.Enable = false;
                user.Createdon = DateTime.Now;
                user.Roles_id = context.roles.Find(1);
                user.user_Name = "udo";
                user.Password = "sdo";

                context.user.Add(user);
                context.SaveChanges();

                user.Enable = true;
                user.Createdon = DateTime.Now;
                user.Roles_id = context.roles.Find(1);
                user.user_Name = "suo";
                user.Password = "sudoko";

                context.user.Add(user);
                context.SaveChanges();

                user.Enable = false;
                user.Createdon = DateTime.Now;
                user.Roles_id = context.roles.Find(1);
                user.user_Name = "suvvvvvdo";
                user.Password = "sudkko";

                context.user.Add(user);
                context.SaveChanges();

                user.Enable = true;
                user.Createdon = DateTime.Now;
                user.Roles_id = context.roles.Find(1);
                user.user_Name = "sudo";
                user.Password = "sudo";

                context.user.Add(user);
                context.SaveChanges();
            }
            //just remove all other entries which are not with id 5
            List<Models.City> cities = new List<Models.City>();
            if (!context.city.Any())
            {
                Models.City city = new Models.City();
                city.city_name = "Karachi";
                city.city_code = "01";
                city.city_name = "Lahore";
                city.city_code = "02";
                city.city_name = "Islamabad";
                city.city_code = "03";
                context.city.AddRange(cities);
                context.SaveChanges();
            }



        }
    }
}
