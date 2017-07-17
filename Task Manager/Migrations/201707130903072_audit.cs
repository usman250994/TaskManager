namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLogs", "activityId", c => c.Int(nullable: false));
            AddColumn("dbo.AuditLogs", "createdBy", c => c.String());
            DropColumn("dbo.AuditLogs", "description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditLogs", "description", c => c.String());
            DropColumn("dbo.AuditLogs", "createdBy");
            DropColumn("dbo.AuditLogs", "activityId");
        }
    }
}
