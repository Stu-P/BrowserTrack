using System.ComponentModel.DataAnnotations;

namespace BrowserTrack.WebApi.Models
{
    public class EnquiryRequestModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Enquiry")]
        public string Enquiry { get; set; }
    }
}