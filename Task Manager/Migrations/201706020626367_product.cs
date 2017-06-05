namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "created_by_id", "dbo.Users");
            DropIndex("dbo.Products", new[] { "created_by_id" });
            DropColumn("dbo.Products", "created_by_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "created_by_id", c => c.Int());
            CreateIndex("dbo.Products", "created_by_id");
            AddForeignKey("dbo.Products", "created_by_id", "dbo.Users", "id");
        }
    }
}
