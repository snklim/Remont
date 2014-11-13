namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Columns", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tables", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customer", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderStatus", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderStatus", "IsDeleted");
            DropColumn("dbo.Order", "IsDeleted");
            DropColumn("dbo.Customer", "IsDeleted");
            DropColumn("dbo.Tables", "IsDeleted");
            DropColumn("dbo.Columns", "IsDeleted");
        }
    }
}
