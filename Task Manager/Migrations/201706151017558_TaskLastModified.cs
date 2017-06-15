namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskLastModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "LastModify", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "LastModify");
        }
    }
}
