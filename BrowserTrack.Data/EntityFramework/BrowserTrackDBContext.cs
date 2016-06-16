using BrowserTrack.Data.Entities;
using System.Data.Entity;
using Microsoft.Azure; 

namespace BrowserTrack.Data.EntityFramework

{
    public class BrowserTrackDBContext : DbContext
    {

        public BrowserTrackDBContext() : base("Name=BrowserTrackConnectionCloud") { }

        public virtual IDbSet<Browser> Browsers { get; set; }
        public virtual IDbSet<VersionChange> VersionChanges { get; set; }
    }
}
