using BrowserTrack.Data.Entities;
using System;
using System.Linq;
namespace BrowserTrack.Data.Repositories
{
    public interface IBrowserTrackRepository
    {
        IQueryable<Browser> GetAllBrowsers();
        Browser GetBrowserSearchDetails(int id);
        IQueryable<Browser> GetAllEligibleBrowserSearchDetails();
        IQueryable<VersionChange> GetAllVersionChanges();

        bool AddVersionChange(VersionChange change);
        bool Save();
        void Update(Browser browser);

        void ChangeEnabledState(int id, bool newState);
    }
}
