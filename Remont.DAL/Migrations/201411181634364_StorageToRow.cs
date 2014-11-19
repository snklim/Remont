namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageToRow : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Storage", newName: "Row");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Row", newName: "Storage");
        }
    }
}
