namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class muqeet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Int(nullable: false, identity: true),
                        customer_name = c.String(),
                        address = c.String(),
                        Phonenumber = c.String(),
                        Website = c.String(),
                        Email = c.String(),
                        enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.customerId);
            
            CreateTable(
                "dbo.CustomerContactDetails",
                c => new
                    {
                        customerContactDetailId = c.Int(nullable: false, identity: true),
                        project_manager = c.String(),
                        contact_number = c.String(),
                        email = c.String(),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.customerContactDetailId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Project_Name = c.String(),
                        Created_On = c.DateTime(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        Created_By_id = c.Int(),
                        customer_customerId = c.Int(),
                        customerContactDetail_customerContactDetailId = c.Int(),
                        projectManager_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.Created_By_id)
                .ForeignKey("dbo.Customers", t => t.customer_customerId)
                .ForeignKey("dbo.CustomerContactDetails", t => t.customerContactDetail_customerContactDetailId)
                .ForeignKey("dbo.Users", t => t.projectManager_id)
                .Index(t => t.Created_By_id)
                .Index(t => t.customer_customerId)
                .Index(t => t.customerContactDetail_customerContactDetailId)
                .Index(t => t.projectManager_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_Name = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        Createdon = c.DateTime(nullable: false),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Taggings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        project_id = c.Int(),
                        tasks_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Projects", t => t.project_id)
                .ForeignKey("dbo.Tasks", t => t.tasks_id)
                .Index(t => t.project_id)
                .Index(t => t.tasks_id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        task_name = c.String(),
                        description = c.String(),
                        created_on = c.DateTime(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        sms = c.Boolean(nullable: false),
                        email = c.Boolean(nullable: false),
                        status = c.Int(nullable: false),
                        enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        role_name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TaggingUsers",
                c => new
                    {
                        Tagging_id = c.Int(nullable: false),
                        Users_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tagging_id, t.Users_id })
                .ForeignKey("dbo.Taggings", t => t.Tagging_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_id, cascadeDelete: true)
                .Index(t => t.Tagging_id)
                .Index(t => t.Users_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "projectManager_id", "dbo.Users");
            DropForeignKey("dbo.Projects", "customerContactDetail_customerContactDetailId", "dbo.CustomerContactDetails");
            DropForeignKey("dbo.Projects", "customer_customerId", "dbo.Customers");
            DropForeignKey("dbo.Projects", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.TaggingUsers", "Users_id", "dbo.Users");
            DropForeignKey("dbo.TaggingUsers", "Tagging_id", "dbo.Taggings");
            DropForeignKey("dbo.Taggings", "tasks_id", "dbo.Tasks");
            DropForeignKey("dbo.Taggings", "project_id", "dbo.Projects");
            DropIndex("dbo.TaggingUsers", new[] { "Users_id" });
            DropIndex("dbo.TaggingUsers", new[] { "Tagging_id" });
            DropIndex("dbo.Taggings", new[] { "tasks_id" });
            DropIndex("dbo.Taggings", new[] { "project_id" });
            DropIndex("dbo.Projects", new[] { "projectManager_id" });
            DropIndex("dbo.Projects", new[] { "customerContactDetail_customerContactDetailId" });
            DropIndex("dbo.Projects", new[] { "customer_customerId" });
            DropIndex("dbo.Projects", new[] { "Created_By_id" });
            DropTable("dbo.TaggingUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Requests");
            DropTable("dbo.Tasks");
            DropTable("dbo.Taggings");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.CustomerContactDetails");
            DropTable("dbo.Customers");
        }
    }
}
