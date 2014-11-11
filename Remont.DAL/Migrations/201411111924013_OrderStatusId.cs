namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatusId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "OrderStatus_Id", "dbo.OrderStatus");
            DropIndex("dbo.Order", new[] { "OrderStatus_Id" });
            RenameColumn(table: "dbo.Order", name: "OrderStatus_Id", newName: "OrderStatusId");
            AlterColumn("dbo.Order", "OrderStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "OrderStatusId");
            AddForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus");
            DropIndex("dbo.Order", new[] { "OrderStatusId" });
            AlterColumn("dbo.Order", "OrderStatusId", c => c.Int());
            RenameColumn(table: "dbo.Order", name: "OrderStatusId", newName: "OrderStatus_Id");
            CreateIndex("dbo.Order", "OrderStatus_Id");
            AddForeignKey("dbo.Order", "OrderStatus_Id", "dbo.OrderStatus", "Id");
        }
    }
}
