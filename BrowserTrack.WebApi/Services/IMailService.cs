using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTrack.WebApi.Services
{
    public interface IMailService
    {

        MailSentStatus SendMail(MailEnquiry enquiry);
    }
}
