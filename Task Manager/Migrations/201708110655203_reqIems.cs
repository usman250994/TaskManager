namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reqIems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.requisitionItems", "approvedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.requisitionItems", "issued_received_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.requisitionItems", "approvedBy_id", c => c.Int());
            AddColumn("dbo.requisitionItems", "issuedBy_id", c => c.Int());
            AddColumn("dbo.requisitionItems", "receivedBy_id", c => c.Int());
            CreateIndex("dbo.requisitionItems", "approvedBy_id");
            CreateIndex("dbo.requisitionItems", "issuedBy_id");
            CreateIndex("dbo.requisitionItems", "receivedBy_id");
            AddForeignKey("dbo.requisitionItems", "approvedBy_id", "dbo.Users", "id");
            AddForeignKey("dbo.requisitionItems", "issuedBy_id", "dbo.Users", "id");
            AddForeignKey("dbo.requisitionItems", "receivedBy_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.requisitionItems", "receivedBy_id", "dbo.Users");
            DropForeignKey("dbo.requisitionItems", "issuedBy_id", "dbo.Users");
            DropForeignKey("dbo.requisitionItems", "approvedBy_id", "dbo.Users");
            DropIndex("dbo.requisitionItems", new[] { "receivedBy_id" });
            DropIndex("dbo.requisitionItems", new[] { "issuedBy_id" });
            DropIndex("dbo.requisitionItems", new[] { "approvedBy_id" });
            DropColumn("dbo.requisitionItems", "receivedBy_id");
            DropColumn("dbo.requisitionItems", "issuedBy_id");
            DropColumn("dbo.requisitionItems", "approvedBy_id");
            DropColumn("dbo.requisitionItems", "issued_received_Date");
            DropColumn("dbo.requisitionItems", "approvedDate");
        }
    }
}
