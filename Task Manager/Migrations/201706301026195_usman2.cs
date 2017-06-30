namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "note", c => c.String());
            AddColumn("dbo.Tasks", "closingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "closingDate");
            DropColumn("dbo.Tasks", "note");
        }
    }
}
