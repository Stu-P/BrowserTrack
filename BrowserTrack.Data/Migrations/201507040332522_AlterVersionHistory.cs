namespace BrowserTrack.Data.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterVersionHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VersionChanges", "Browser_Id", "dbo.Browsers");
            DropIndex("dbo.VersionChanges", new[] { "Browser_Id" });
            AddColumn("dbo.VersionChanges", "BrowserId", c => c.Int(nullable: false));
            DropColumn("dbo.VersionChanges", "Browser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VersionChanges", "Browser_Id", c => c.Int());
            DropColumn("dbo.VersionChanges", "BrowserId");
            CreateIndex("dbo.VersionChanges", "Browser_Id");
            AddForeignKey("dbo.VersionChanges", "Browser_Id", "dbo.Browsers", "Id");
        }
    }
}
