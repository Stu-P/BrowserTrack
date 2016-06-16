using System;
using System.Collections.Generic;

namespace BrowserTrack.Data.Models
{
    public partial class Browser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OS { get; set; }
        public string CurrentVersion { get; set; }
        public Nullable<System.DateTime> LastVersionCheck { get; set; }
        public int SearchCriteria_Id { get; set; }
        public bool VersionCheckEnabled { get; set; }
        public virtual SearchCriteria SearchCriteria { get; set; }
    }
}
