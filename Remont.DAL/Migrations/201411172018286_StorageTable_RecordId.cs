namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageTable_RecordId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storage", "RecordId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Storage", "RecordId");
        }
    }
}
