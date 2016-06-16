using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserTrack.WebApi.Services
{
    public class MailEnquiry
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Enquiry { get; set; }
    }
}