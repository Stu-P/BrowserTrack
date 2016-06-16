using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserTrack.WebApi.Services
{
/// <summary>
/// A mail service with simple business logic, if outside business hours the mail is delayed.
/// </summary>
    public class BusinessHoursMailService : IMailService
    {
        private TimeSpan startOfDay = new TimeSpan(9, 0, 0);
        private TimeSpan endOfDay = new TimeSpan(17, 0, 0);

        public MailSentStatus SendMail(MailEnquiry enquiry)
        {

            if (enquiry == null)
                return MailSentStatus.Failed;

            // Allow UI test to simulate a failure
            if (enquiry.FirstName == "FAIL") return MailSentStatus.Failed;

            if (TimeBetween(DateTime.Now, startOfDay, endOfDay))
                return MailSentStatus.OK;
            else
                return MailSentStatus.Sheduled;
        }

        private bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            TimeSpan now = datetime.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }
    }
}