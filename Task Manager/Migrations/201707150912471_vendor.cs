namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        vendorName = c.String(),
                        address = c.String(),
                        city = c.String(),
                        mobNo = c.String(),
                        teleNo = c.String(),
                        Email = c.String(),
                        website = c.String(),
                        fax = c.String(),
                        isApprove = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        creditLimit = c.String(),
                        days = c.Int(nullable: false),
                        preferredCourier = c.String(),
                        syestemCategory = c.String(),
                        enable = c.Boolean(nullable: false),
                        createdBy_id = c.Int(),
                        type_id = c.Int(),
                        vendorContact_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.createdBy_id)
                .ForeignKey("dbo.VendorTypes", t => t.type_id)
                .ForeignKey("dbo.VendorContacts", t => t.vendorContact_id)
                .Index(t => t.createdBy_id)
                .Index(t => t.type_id)
                .Index(t => t.vendorContact_id);
            
            CreateTable(
                "dbo.VendorTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type_Name = c.String(),
                        enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.VendorContacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        regNo = c.String(),
                        nic = c.String(),
                        title = c.String(),
                        email = c.String(),
                        mobNo = c.String(),
                        telephoneNo = c.String(),
                        fax = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendors", "vendorContact_id", "dbo.VendorContacts");
            DropForeignKey("dbo.Vendors", "type_id", "dbo.VendorTypes");
            DropForeignKey("dbo.Vendors", "createdBy_id", "dbo.Users");
            DropIndex("dbo.Vendors", new[] { "vendorContact_id" });
            DropIndex("dbo.Vendors", new[] { "type_id" });
            DropIndex("dbo.Vendors", new[] { "createdBy_id" });
            DropTable("dbo.VendorContacts");
            DropTable("dbo.VendorTypes");
            DropTable("dbo.Vendors");
        }
    }
}
