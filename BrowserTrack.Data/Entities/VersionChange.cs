using System;
using System.ComponentModel.DataAnnotations;

namespace BrowserTrack.Data.Entities
{
    public class VersionChange
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BrowserName { get; set; }
        
        [Required]
        public string NewVersion { get; set; }
        
        [Required]
        public string PriorVersion { get; set; }
        
        [Required]
        public DateTime DateOfChange { get; set; }
        

    }
}
