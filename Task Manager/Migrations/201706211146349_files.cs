namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class files : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "createdBy_id", "dbo.Users");
            DropIndex("dbo.Files", new[] { "createdBy_id" });
            AddColumn("dbo.Files", "Project_id", c => c.Int());
            CreateIndex("dbo.Files", "Project_id");
            AddForeignKey("dbo.Files", "Project_id", "dbo.Projects", "id");
            DropColumn("dbo.Files", "createdBy_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "createdBy_id", c => c.Int());
            DropForeignKey("dbo.Files", "Project_id", "dbo.Projects");
            DropIndex("dbo.Files", new[] { "Project_id" });
            DropColumn("dbo.Files", "Project_id");
            CreateIndex("dbo.Files", "createdBy_id");
            AddForeignKey("dbo.Files", "createdBy_id", "dbo.Users", "id");
        }
    }
}
