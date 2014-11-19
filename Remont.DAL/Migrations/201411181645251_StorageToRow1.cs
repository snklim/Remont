namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageToRow1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Row", "ValueType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Row", "ValueType", c => c.String());
        }
    }
}
