namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "OrderStatus_Id", c => c.Int());
            CreateIndex("dbo.Order", "OrderStatus_Id");
            AddForeignKey("dbo.Order", "OrderStatus_Id", "dbo.OrderStatus", "Id");
            DropColumn("dbo.Order", "OrderStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "OrderStatus", c => c.Int(nullable: false));
            DropForeignKey("dbo.Order", "OrderStatus_Id", "dbo.OrderStatus");
            DropIndex("dbo.Order", new[] { "OrderStatus_Id" });
            DropColumn("dbo.Order", "OrderStatus_Id");
            DropTable("dbo.OrderStatus");
        }
    }
}
