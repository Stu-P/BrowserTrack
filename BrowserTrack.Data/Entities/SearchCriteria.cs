using System;
using System.ComponentModel.DataAnnotations;

namespace BrowserTrack.Data.Entities
{
    public class SearchCriteria
    {

        [Key]
        public int Id { get; set; }



        [Required]
        public string Url { get; set; }

        [Required]
        public string PageLocator { get; set; }

        [Required]
        public string VersionRegex { get; set; }
    }
}
