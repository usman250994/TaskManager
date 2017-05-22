namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagingServiceModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        timeStamp = c.DateTime(nullable: false),
                        sentBy_id = c.Int(),
                        Task_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.sentBy_id)
                .ForeignKey("dbo.Tasks", t => t.Task_id)
                .Index(t => t.sentBy_id)
                .Index(t => t.Task_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.messages", "Task_id", "dbo.Tasks");
            DropForeignKey("dbo.messages", "sentBy_id", "dbo.Users");
            DropIndex("dbo.messages", new[] { "Task_id" });
            DropIndex("dbo.messages", new[] { "sentBy_id" });
            DropTable("dbo.messages");
        }
    }
}
