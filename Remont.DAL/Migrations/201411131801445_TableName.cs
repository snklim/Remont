namespace Remont.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Columns", newName: "Column");
            RenameTable(name: "dbo.Tables", newName: "Table");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Table", newName: "Tables");
            RenameTable(name: "dbo.Column", newName: "Columns");
        }
    }
}
