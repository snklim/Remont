namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Column", "TableId", "dbo.Table");
            DropForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus");
            CreateTable(
                "dbo.Storage",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                        ColumnId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Column", t => t.Id)
                .ForeignKey("dbo.Table", t => t.Id)
                .Index(t => t.Id);
            
            AddForeignKey("dbo.Column", "TableId", "dbo.Table", "Id");
            AddForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Column", "TableId", "dbo.Table");
            DropForeignKey("dbo.Storage", "Id", "dbo.Table");
            DropForeignKey("dbo.Storage", "Id", "dbo.Column");
            DropIndex("dbo.Storage", new[] { "Id" });
            DropTable("dbo.Storage");
            AddForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Column", "TableId", "dbo.Table", "Id", cascadeDelete: true);
        }
    }
}
