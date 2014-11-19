namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Row", "Id", "dbo.Column");
            DropForeignKey("dbo.Row", "Id", "dbo.Table");
            DropIndex("dbo.Row", new[] { "Id" });
            DropPrimaryKey("dbo.Row");
            AlterColumn("dbo.Row", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Row", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Row");
            AlterColumn("dbo.Row", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Row", "Id");
            CreateIndex("dbo.Row", "Id");
            AddForeignKey("dbo.Row", "Id", "dbo.Table", "Id");
            AddForeignKey("dbo.Row", "Id", "dbo.Column", "Id");
        }
    }
}
