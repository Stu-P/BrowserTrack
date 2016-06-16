using System;
using System.Collections.Generic;

namespace BrowserTrack.Data.Models
{
    public partial class VersionChange
    {
        public int Id { get; set; }
        public string NewVersion { get; set; }
        public string PriorVersion { get; set; }
        public System.DateTime DateOfChange { get; set; }
        public string BrowserName { get; set; }
    }
}
