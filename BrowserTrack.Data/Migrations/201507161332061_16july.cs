namespace BrowserTrack.Data.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16july : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Browsers", "VersionCheckEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.SearchCriterias", "VersionCheckEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchCriterias", "VersionCheckEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Browsers", "VersionCheckEnabled");
        }
    }
}
