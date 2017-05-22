namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.messages", "location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.messages", "location");
        }
    }
}
