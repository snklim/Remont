namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowTable2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Row", "TableId");
            CreateIndex("dbo.Row", "ColumnId");
            AddForeignKey("dbo.Row", "ColumnId", "dbo.Column", "Id");
            AddForeignKey("dbo.Row", "TableId", "dbo.Table", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Row", "TableId", "dbo.Table");
            DropForeignKey("dbo.Row", "ColumnId", "dbo.Column");
            DropIndex("dbo.Row", new[] { "ColumnId" });
            DropIndex("dbo.Row", new[] { "TableId" });
        }
    }
}
