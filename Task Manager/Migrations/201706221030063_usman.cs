namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city_name = c.String(),
                        city_code = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Int(nullable: false, identity: true),
                        customer_name = c.String(),
                        address = c.String(),
                        OnBoarddate = c.DateTime(nullable: false),
                        Phonenumber = c.String(),
                        Website = c.String(),
                        Email = c.String(),
                        enable = c.Boolean(nullable: false),
                        city_code = c.String(),
                        createdOn = c.DateTime(nullable: false),
                        Created_By_id = c.Int(),
                    })
                .PrimaryKey(t => t.customerId)
                .ForeignKey("dbo.Users", t => t.Created_By_id)
                .Index(t => t.Created_By_id);
            
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
                        Created_By_id = c.Int(),
                        Roles_id_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.Created_By_id)
                .ForeignKey("dbo.Roles", t => t.Roles_id_id)
                .Index(t => t.Created_By_id)
                .Index(t => t.Roles_id_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        role_name = c.String(),
                        enable = c.Boolean(nullable: false),
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
                "dbo.Projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Project_Name = c.String(),
                        work_order = c.Int(nullable: false),
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
                "dbo.Files",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fileName = c.String(),
                        fileCode = c.String(),
                        filetype = c.String(),
                        createdOn = c.DateTime(nullable: false),
                        Project_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Projects", t => t.Project_id)
                .Index(t => t.Project_id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        task_name = c.String(),
                        description = c.String(),
                        branch_code = c.String(),
                        created_on = c.DateTime(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        LastModify = c.DateTime(nullable: false),
                        sms = c.Boolean(nullable: false),
                        email = c.Boolean(nullable: false),
                        status = c.Int(nullable: false),
                        enable = c.Boolean(nullable: false),
                        IsTicket = c.Boolean(nullable: false),
                        Created_By_id = c.Int(),
                        ticketContact_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.Created_By_id)
                .ForeignKey("dbo.ticketContacts", t => t.ticketContact_id)
                .Index(t => t.Created_By_id)
                .Index(t => t.ticketContact_id);
            
            CreateTable(
                "dbo.messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        timeStamp = c.DateTime(nullable: false),
                        location = c.String(),
                        sentBy_id = c.Int(),
                        Task_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.sentBy_id)
                .ForeignKey("dbo.Tasks", t => t.Task_id)
                .Index(t => t.sentBy_id)
                .Index(t => t.Task_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_name = c.String(),
                        islocal = c.Boolean(nullable: false),
                        model = c.String(),
                        brand = c.String(),
                        description = c.String(),
                        quantity = c.Int(nullable: false),
                        barcode = c.String(),
                        vendor_name = c.String(),
                        image = c.String(),
                        enable = c.Boolean(nullable: false),
                        inward_date = c.DateTime(nullable: false),
                        created_on = c.DateTime(nullable: false),
                        category_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.category_id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.category_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ticketContacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contact = c.String(),
                        email = c.String(),
                        name = c.String(),
                        enable = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.Tasks", "ticketContact_id", "dbo.ticketContacts");
            DropForeignKey("dbo.Products", "user_id", "dbo.Users");
            DropForeignKey("dbo.Products", "category_id", "dbo.Categories");
            DropForeignKey("dbo.Customers", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.TaggingUsers", "Users_id", "dbo.Users");
            DropForeignKey("dbo.TaggingUsers", "Tagging_id", "dbo.Taggings");
            DropForeignKey("dbo.Taggings", "tasks_id", "dbo.Tasks");
            DropForeignKey("dbo.messages", "Task_id", "dbo.Tasks");
            DropForeignKey("dbo.messages", "sentBy_id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.Taggings", "project_id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "projectManager_id", "dbo.Users");
            DropForeignKey("dbo.Files", "Project_id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "customerContactDetail_customerContactDetailId", "dbo.CustomerContactDetails");
            DropForeignKey("dbo.Projects", "customer_customerId", "dbo.Customers");
            DropForeignKey("dbo.Projects", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.Users", "Roles_id_id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Created_By_id", "dbo.Users");
            DropIndex("dbo.TaggingUsers", new[] { "Users_id" });
            DropIndex("dbo.TaggingUsers", new[] { "Tagging_id" });
            DropIndex("dbo.Products", new[] { "user_id" });
            DropIndex("dbo.Products", new[] { "category_id" });
            DropIndex("dbo.messages", new[] { "Task_id" });
            DropIndex("dbo.messages", new[] { "sentBy_id" });
            DropIndex("dbo.Tasks", new[] { "ticketContact_id" });
            DropIndex("dbo.Tasks", new[] { "Created_By_id" });
            DropIndex("dbo.Files", new[] { "Project_id" });
            DropIndex("dbo.Projects", new[] { "projectManager_id" });
            DropIndex("dbo.Projects", new[] { "customerContactDetail_customerContactDetailId" });
            DropIndex("dbo.Projects", new[] { "customer_customerId" });
            DropIndex("dbo.Projects", new[] { "Created_By_id" });
            DropIndex("dbo.Taggings", new[] { "tasks_id" });
            DropIndex("dbo.Taggings", new[] { "project_id" });
            DropIndex("dbo.Users", new[] { "Roles_id_id" });
            DropIndex("dbo.Users", new[] { "Created_By_id" });
            DropIndex("dbo.Customers", new[] { "Created_By_id" });
            DropTable("dbo.TaggingUsers");
            DropTable("dbo.ticketContacts");
            DropTable("dbo.Requests");
            DropTable("dbo.Products");
            DropTable("dbo.messages");
            DropTable("dbo.Tasks");
            DropTable("dbo.Files");
            DropTable("dbo.CustomerContactDetails");
            DropTable("dbo.Projects");
            DropTable("dbo.Taggings");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Cities");
            DropTable("dbo.Categories");
        }
    }
}
