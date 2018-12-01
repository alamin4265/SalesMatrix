namespace SalesMatrix.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Database_Create : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoleTables", newName: "Roles");
            RenameTable(name: "dbo.UserTables", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "UserTables");
            RenameTable(name: "dbo.Roles", newName: "RoleTables");
        }
    }
}
