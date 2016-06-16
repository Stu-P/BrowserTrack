using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrowserTrack.Data.Repositories;
using BrowserTrack.Data.Entities;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using BrowserTrack.WebApi.Models;
using BrowserTrack.WebApi.Services;

namespace BrowserTrack.WebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactController : ApiController
    {

        private IMailService _mailService;

        public ContactController(IMailService mailService) {
            _mailService = mailService;
        }


        [Authorize]
        public async Task<IHttpActionResult> Enquiry(EnquiryRequestModel request) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Add a fake delay to simulate processing time
            System.Threading.Thread.Sleep(2000);

            var newEnquiry = AutoMapper.Mapper.Map<EnquiryRequestModel, MailEnquiry>(request);

            var sent = _mailService.SendMail(newEnquiry);

            if (sent == MailSentStatus.Failed)
                return BadRequest();

            else return Ok();
           

        }


    }
}
