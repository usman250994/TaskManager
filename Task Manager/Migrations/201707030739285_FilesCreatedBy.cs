namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilesCreatedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "createdBy_id", c => c.Int());
            CreateIndex("dbo.Files", "createdBy_id");
            AddForeignKey("dbo.Files", "createdBy_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "createdBy_id", "dbo.Users");
            DropIndex("dbo.Files", new[] { "createdBy_id" });
            DropColumn("dbo.Files", "createdBy_id");
        }
    }
}
