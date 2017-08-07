namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requisition_issued : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "createdDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requisitions", "approvedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requisitions", "issuedTo_id", c => c.Int());
            CreateIndex("dbo.Requisitions", "issuedTo_id");
            AddForeignKey("dbo.Requisitions", "issuedTo_id", "dbo.Users", "id");
            DropColumn("dbo.Requisitions", "createdOn");
            DropColumn("dbo.Requisitions", "approvedOn");
            DropColumn("dbo.Requisitions", "issuedTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requisitions", "issuedTo", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requisitions", "approvedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requisitions", "createdOn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Requisitions", "issuedTo_id", "dbo.Users");
            DropIndex("dbo.Requisitions", new[] { "issuedTo_id" });
            DropColumn("dbo.Requisitions", "issuedTo_id");
            DropColumn("dbo.Requisitions", "approvedDate");
            DropColumn("dbo.Requisitions", "createdDate");
        }
    }
}
