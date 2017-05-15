namespace Task_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usman : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tasks", name: "customer_id", newName: "ticketContact_id");
            RenameIndex(table: "dbo.Tasks", name: "IX_customer_id", newName: "IX_ticketContact_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tasks", name: "IX_ticketContact_id", newName: "IX_customer_id");
            RenameColumn(table: "dbo.Tasks", name: "ticketContact_id", newName: "customer_id");
        }
    }
}
