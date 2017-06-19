namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "OnBoarddate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "createdOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "createdOn");
            DropColumn("dbo.Customers", "OnBoarddate");
        }
    }
}
