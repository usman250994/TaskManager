namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auditlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        actionCode = c.String(),
                        description = c.String(),
                        tableName = c.String(),
                        activityDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
