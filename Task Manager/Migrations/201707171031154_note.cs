namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class note : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendors", "note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendors", "note");
        }
    }
}
