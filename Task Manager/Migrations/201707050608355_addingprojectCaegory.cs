namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingprojectCaegory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "categroy_id", c => c.Int());
            CreateIndex("dbo.Projects", "categroy_id");
            AddForeignKey("dbo.Projects", "categroy_id", "dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "categroy_id", "dbo.Categories");
            DropIndex("dbo.Projects", new[] { "categroy_id" });
            DropColumn("dbo.Projects", "categroy_id");
        }
    }
}
