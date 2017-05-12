namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectwithuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "projectManager_id", c => c.Int());
            CreateIndex("dbo.Projects", "projectManager_id");
            AddForeignKey("dbo.Projects", "projectManager_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "projectManager_id", "dbo.Users");
            DropIndex("dbo.Projects", new[] { "projectManager_id" });
            DropColumn("dbo.Projects", "projectManager_id");
        }
    }
}
