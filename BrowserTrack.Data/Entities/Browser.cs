using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace BrowserTrack.Data.Entities
{
    public class Browser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OS { get; set; }


                
        [Required]
        public string CurrentVersion { get; set; }

        public DateTime? LastVersionCheck { get; set; }

        [Required]
        public bool VersionCheckEnabled { get; set; }

        [Required]
        public virtual SearchCriteria SearchCriteria { get; set; }


    }
}
