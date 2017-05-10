namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "enable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "enable");
        }
    }
}
