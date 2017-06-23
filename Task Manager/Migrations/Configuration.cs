namespace Task_Manager.Migrations
{
    using System;
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
            Task_Manager.Models.Roles role = new Models.Roles();

            role.enable = false;
            role.role_name = "Sudo";
            context.roles.Add(role);
            context.SaveChanges();


            role.enable = true;
            role.role_name = "Admin";
            context.roles.Add(role);
            context.SaveChanges();

            role.role_name = "Normal";
            context.roles.Add(role);
            context.SaveChanges();


            role.role_name = "CSR";
            context.roles.Add(role);
            context.SaveChanges();




            // Seeding Users Table

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

            //just remove all other entries which are not with id 5

            Models.City city = new Models.City();

            city.city_name = "Karachi";
            city.city_code = "01";


            context.city.Add(city);
            context.SaveChanges();

            city.city_name = "Lahore";
            city.city_code = "02";

            context.city.Add(city);
            context.SaveChanges();

            city.city_name = "Islamabad";
            city.city_code = "03";

            context.city.Add(city);
            context.SaveChanges();
        }
    }
}
