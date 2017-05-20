namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fahad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "enable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "enable");
        }
    }
}
