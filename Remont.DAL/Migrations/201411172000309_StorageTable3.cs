namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storage", "Value", c => c.String());
            AddColumn("dbo.Storage", "ValueType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Storage", "ValueType");
            DropColumn("dbo.Storage", "Value");
        }
    }
}
