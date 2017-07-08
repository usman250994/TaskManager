namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusDocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusDocuments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        documentPath = c.String(),
                        createdOn = c.DateTime(nullable: false),
                        createdBy_id = c.Int(),
                        Task_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.createdBy_id)
                .ForeignKey("dbo.Tasks", t => t.Task_id)
                .Index(t => t.createdBy_id)
                .Index(t => t.Task_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusDocuments", "Task_id", "dbo.Tasks");
            DropForeignKey("dbo.StatusDocuments", "createdBy_id", "dbo.Users");
            DropIndex("dbo.StatusDocuments", new[] { "Task_id" });
            DropIndex("dbo.StatusDocuments", new[] { "createdBy_id" });
            DropTable("dbo.StatusDocuments");
        }
    }
}
