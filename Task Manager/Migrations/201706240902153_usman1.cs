namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ticket_code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "ticket_code");
        }
    }
}
