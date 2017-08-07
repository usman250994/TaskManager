namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requisition_new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requisitions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Sno = c.String(),
                        itemName = c.String(),
                        itemCode = c.String(),
                        units = c.String(),
                        requiredQuantity = c.String(),
                        issuedQuanatity = c.String(),
                        requiredDate = c.DateTime(nullable: false),
                        createdOn = c.DateTime(nullable: false),
                        approvedOn = c.DateTime(nullable: false),
                        issuedTo = c.DateTime(nullable: false),
                        issuedDate = c.DateTime(nullable: false),
                        enable = c.Boolean(nullable: false),
                        approvedBy_id = c.Int(),
                        createdBy_id = c.Int(),
                        customer_customerId = c.Int(),
                        issuedBy_id = c.Int(),
                        project_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.approvedBy_id)
                .ForeignKey("dbo.Users", t => t.createdBy_id)
                .ForeignKey("dbo.Customers", t => t.customer_customerId)
                .ForeignKey("dbo.Users", t => t.issuedBy_id)
                .ForeignKey("dbo.Projects", t => t.project_id)
                .Index(t => t.approvedBy_id)
                .Index(t => t.createdBy_id)
                .Index(t => t.customer_customerId)
                .Index(t => t.issuedBy_id)
                .Index(t => t.project_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitions", "project_id", "dbo.Projects");
            DropForeignKey("dbo.Requisitions", "issuedBy_id", "dbo.Users");
            DropForeignKey("dbo.Requisitions", "customer_customerId", "dbo.Customers");
            DropForeignKey("dbo.Requisitions", "createdBy_id", "dbo.Users");
            DropForeignKey("dbo.Requisitions", "approvedBy_id", "dbo.Users");
            DropIndex("dbo.Requisitions", new[] { "project_id" });
            DropIndex("dbo.Requisitions", new[] { "issuedBy_id" });
            DropIndex("dbo.Requisitions", new[] { "customer_customerId" });
            DropIndex("dbo.Requisitions", new[] { "createdBy_id" });
            DropIndex("dbo.Requisitions", new[] { "approvedBy_id" });
            DropTable("dbo.Requisitions");
        }
    }
}
