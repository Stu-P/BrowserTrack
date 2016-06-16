using BrowserTrack.Data.Entities;
using System;
using System.Linq;
using BrowserTrack.Data.EntityFramework;
using System.Data.Entity;

namespace BrowserTrack.Data.Repositories
{
    public class BrowserTrackRepository : IBrowserTrackRepository 
    {
//        Scraper 
//- Get a list of Browsers eligible for version check, and search criteria
//- Update the version for browser
//- send a update history message (audit)

        private BrowserTrackDBContext _context;

        public BrowserTrackRepository(BrowserTrackDBContext context)
        {
            _context = context;
        }


        public IQueryable<Browser> GetAllBrowsers()
        {
            return _context.Browsers;
            
        }

        public IQueryable<VersionChange> GetAllVersionChanges() {
            return _context.VersionChanges.OrderByDescending(v => v.DateOfChange);

        }


        public Browser GetBrowserSearchDetails(int id)
        {

            return _context.Browsers.Include("SearchCriteria").FirstOrDefault<Browser>(b => b.Id == id);
          
        }

        public IQueryable<Browser> GetAllEligibleBrowserSearchDetails()
        {
                return _context.Browsers.Include("SearchCriteria").Where(b => b.VersionCheckEnabled == true);
        }

        public void Update(Browser browser)
        {
            _context.Entry(browser).State = System.Data.Entity.EntityState.Modified;
        }


        public bool AddVersionChange(VersionChange change)
        {
            try
            {
                _context.VersionChanges.Add(change);
                return true;

            }
            catch (Exception ex) { 
                //TODO: Log error
                return false;
            }
        }



        public void ChangeEnabledState(int id, bool newState) {

            _context.Browsers.FirstOrDefault<Browser>(b => b.Id == id).VersionCheckEnabled = newState;


        }
        


        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }


    }
}
