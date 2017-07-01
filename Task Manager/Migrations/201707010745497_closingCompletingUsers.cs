namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class closingCompletingUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "closingUser_id", c => c.Int());
            AddColumn("dbo.Tasks", "completingUser_id", c => c.Int());
            CreateIndex("dbo.Tasks", "closingUser_id");
            CreateIndex("dbo.Tasks", "completingUser_id");
            AddForeignKey("dbo.Tasks", "closingUser_id", "dbo.Users", "id");
            AddForeignKey("dbo.Tasks", "completingUser_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "completingUser_id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "closingUser_id", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "completingUser_id" });
            DropIndex("dbo.Tasks", new[] { "closingUser_id" });
            DropColumn("dbo.Tasks", "completingUser_id");
            DropColumn("dbo.Tasks", "closingUser_id");
        }
    }
}
