namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketamendments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ticketContacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contact = c.String(),
                        email = c.String(),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Tasks", "customer_id", c => c.Int());
            CreateIndex("dbo.Tasks", "customer_id");
            AddForeignKey("dbo.Tasks", "customer_id", "dbo.ticketContacts", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "customer_id", "dbo.ticketContacts");
            DropIndex("dbo.Tasks", new[] { "customer_id" });
            DropColumn("dbo.Tasks", "customer_id");
            DropTable("dbo.ticketContacts");
        }
    }
}
