namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isClose : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusDocuments", "isClose", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusDocuments", "isClose");
        }
    }
}
