namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Created_By_id", c => c.Int());
            AddColumn("dbo.CustomerContactDetails", "Created_By_id", c => c.Int());
            AddColumn("dbo.Users", "Created_By_id", c => c.Int());
            AddColumn("dbo.Tasks", "Created_By_id", c => c.Int());
            CreateIndex("dbo.Customers", "Created_By_id");
            CreateIndex("dbo.Users", "Created_By_id");
            CreateIndex("dbo.CustomerContactDetails", "Created_By_id");
            CreateIndex("dbo.Tasks", "Created_By_id");
            AddForeignKey("dbo.Users", "Created_By_id", "dbo.Users", "id");
            AddForeignKey("dbo.CustomerContactDetails", "Created_By_id", "dbo.Users", "id");
            AddForeignKey("dbo.Tasks", "Created_By_id", "dbo.Users", "id");
            AddForeignKey("dbo.Customers", "Created_By_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.CustomerContactDetails", "Created_By_id", "dbo.Users");
            DropForeignKey("dbo.Users", "Created_By_id", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Created_By_id" });
            DropIndex("dbo.CustomerContactDetails", new[] { "Created_By_id" });
            DropIndex("dbo.Users", new[] { "Created_By_id" });
            DropIndex("dbo.Customers", new[] { "Created_By_id" });
            DropColumn("dbo.Tasks", "Created_By_id");
            DropColumn("dbo.Users", "Created_By_id");
            DropColumn("dbo.CustomerContactDetails", "Created_By_id");
            DropColumn("dbo.Customers", "Created_By_id");
        }
    }
}
