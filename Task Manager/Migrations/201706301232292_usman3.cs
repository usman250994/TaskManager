namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "completeNote", c => c.String());
            AddColumn("dbo.Tasks", "completeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "completeDate");
            DropColumn("dbo.Tasks", "completeNote");
        }
    }
}
