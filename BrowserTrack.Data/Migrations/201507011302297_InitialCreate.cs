namespace BrowserTrack.Data.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Browsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        OS = c.String(nullable: false),
                        CurrentVersion = c.String(nullable: false),
                        LastVersionCheck = c.DateTime(),
                        SearchInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchCriterias", t => t.SearchInfo_Id, cascadeDelete: true)
                .Index(t => t.SearchInfo_Id);
            
            CreateTable(
                "dbo.SearchCriterias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VersionCheckEnabled = c.Boolean(nullable: false),
                        Url = c.String(nullable: false),
                        PageLocator = c.String(nullable: false),
                        VersionRegex = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VersionChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewVersion = c.String(nullable: false),
                        PriorVersion = c.String(nullable: false),
                        DateOfChange = c.DateTime(nullable: false),
                        Browser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Browsers", t => t.Browser_Id)
                .Index(t => t.Browser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VersionChanges", "Browser_Id", "dbo.Browsers");
            DropForeignKey("dbo.Browsers", "SearchInfo_Id", "dbo.SearchCriterias");
            DropIndex("dbo.VersionChanges", new[] { "Browser_Id" });
            DropIndex("dbo.Browsers", new[] { "SearchInfo_Id" });
            DropTable("dbo.VersionChanges");
            DropTable("dbo.SearchCriterias");
            DropTable("dbo.Browsers");
        }
    }
}
