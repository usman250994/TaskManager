namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "partNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "partNo");
        }
    }
}
