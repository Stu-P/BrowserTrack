namespace BrowserTrack.Data.EntityFramework
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSearchCriteriaName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Browsers", name: "SearchInfo_Id", newName: "SearchCriteria_Id");
            RenameIndex(table: "dbo.Browsers", name: "IX_SearchInfo_Id", newName: "IX_SearchCriteria_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Browsers", name: "IX_SearchCriteria_Id", newName: "IX_SearchInfo_Id");
            RenameColumn(table: "dbo.Browsers", name: "SearchCriteria_Id", newName: "SearchInfo_Id");
        }
    }
}
