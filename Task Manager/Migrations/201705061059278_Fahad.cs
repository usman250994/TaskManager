namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fahad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "address", c => c.String());
            AddColumn("dbo.Customers", "Phonenumber", c => c.String());
            AddColumn("dbo.Customers", "Website", c => c.String());
            AddColumn("dbo.Customers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "Website");
            DropColumn("dbo.Customers", "Phonenumber");
            DropColumn("dbo.Customers", "address");
        }
    }
}
