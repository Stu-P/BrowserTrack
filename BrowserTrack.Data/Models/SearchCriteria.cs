using System;
using System.Collections.Generic;

namespace BrowserTrack.Data.Models
{
    public partial class SearchCriteria
    {
        public SearchCriteria()
        {
            this.Browsers = new List<Browser>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string PageLocator { get; set; }
        public string VersionRegex { get; set; }
        public virtual ICollection<Browser> Browsers { get; set; }
    }
}
