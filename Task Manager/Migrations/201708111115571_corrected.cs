namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corrected : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.requisitionItems", "approvedBy_id", "dbo.Users");
            DropForeignKey("dbo.requisitionItems", "issuedBy_id", "dbo.Users");
            DropForeignKey("dbo.requisitionItems", "receivedBy_id", "dbo.Users");
            DropIndex("dbo.requisitionItems", new[] { "approvedBy_id" });
            DropIndex("dbo.requisitionItems", new[] { "issuedBy_id" });
            DropIndex("dbo.requisitionItems", new[] { "receivedBy_id" });
            AddColumn("dbo.Requisitions", "approvalNote", c => c.String());
            AddColumn("dbo.requisitionItems", "approve", c => c.Boolean(nullable: false));
            DropColumn("dbo.requisitionItems", "approvedDate");
            DropColumn("dbo.requisitionItems", "issued_received_Date");
            DropColumn("dbo.requisitionItems", "approvedBy_id");
            DropColumn("dbo.requisitionItems", "issuedBy_id");
            DropColumn("dbo.requisitionItems", "receivedBy_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.requisitionItems", "receivedBy_id", c => c.Int());
            AddColumn("dbo.requisitionItems", "issuedBy_id", c => c.Int());
            AddColumn("dbo.requisitionItems", "approvedBy_id", c => c.Int());
            AddColumn("dbo.requisitionItems", "issued_received_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.requisitionItems", "approvedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.requisitionItems", "approve");
            DropColumn("dbo.Requisitions", "approvalNote");
            CreateIndex("dbo.requisitionItems", "receivedBy_id");
            CreateIndex("dbo.requisitionItems", "issuedBy_id");
            CreateIndex("dbo.requisitionItems", "approvedBy_id");
            AddForeignKey("dbo.requisitionItems", "receivedBy_id", "dbo.Users", "id");
            AddForeignKey("dbo.requisitionItems", "issuedBy_id", "dbo.Users", "id");
            AddForeignKey("dbo.requisitionItems", "approvedBy_id", "dbo.Users", "id");
        }
    }
}
