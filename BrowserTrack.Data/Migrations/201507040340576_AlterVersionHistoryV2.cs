namespace BrowserTrack.Data.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterVersionHistoryV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VersionChanges", "Browser_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.VersionChanges", "Browser_Id");
            AddForeignKey("dbo.VersionChanges", "Browser_Id", "dbo.Browsers", "Id", cascadeDelete: true);
            DropColumn("dbo.VersionChanges", "BrowserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VersionChanges", "BrowserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.VersionChanges", "Browser_Id", "dbo.Browsers");
            DropIndex("dbo.VersionChanges", new[] { "Browser_Id" });
            DropColumn("dbo.VersionChanges", "Browser_Id");
        }
    }
}
