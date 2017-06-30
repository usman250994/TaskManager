namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filetype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilesTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Filecode = c.String(),
                        Filename = c.String(),
                        enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FilesTypes");
        }
    }
}
