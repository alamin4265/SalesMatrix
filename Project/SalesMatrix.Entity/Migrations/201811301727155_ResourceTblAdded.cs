namespace SalesMatrix.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceTblAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        ResourceType = c.String(),
                        ResourceName = c.String(),
                        Description = c.String(),
                        Parent = c.String(),
                        Sequence = c.String(),
                        ModuleId = c.String(),
                        IsGlobal = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedFrom = c.String(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedFrom = c.String(),
                        Status = c.Boolean(nullable: false),
                        Module_ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.ResourceId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .Index(t => t.Module_ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.Resources", new[] { "Module_ModuleId" });
            DropTable("dbo.Resources");
        }
    }
}
