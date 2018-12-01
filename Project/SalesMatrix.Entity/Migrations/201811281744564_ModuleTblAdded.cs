namespace SalesMatrix.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModuleTblAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedFrom = c.String(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedFrom = c.String(),
                    })
                .PrimaryKey(t => t.ModuleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Modules");
        }
    }
}
