namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityModeladd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city_name = c.String(),
                        city_code = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Customers", "city_code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "city_code");
            DropTable("dbo.Cities");
        }
    }
}
