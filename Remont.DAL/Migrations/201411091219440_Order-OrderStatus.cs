namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderOrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderStatus");
        }
    }
}
