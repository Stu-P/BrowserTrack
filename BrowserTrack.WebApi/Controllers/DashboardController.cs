using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrowserTrack.Data.Repositories;
using BrowserTrack.Data.Entities;
using System.Web.Http.Cors;

namespace BrowserTrack.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {

        private IBrowserTrackRepository browserRepo;

        public DashboardController(IBrowserTrackRepository _browserRepo)
        {
            browserRepo = _browserRepo;
        }


        [HttpGet]
        public IQueryable<Browser> GetAllBrowsers()
        {
            return browserRepo.GetAllBrowsers();
        }

        [HttpGet]
        public Browser GetBrowserSearchDetails(int id)
        {
            return browserRepo.GetBrowserSearchDetails(id);
        }

        [HttpGet]
        public IQueryable<VersionChange> GetAllVersionChanges() {
            return browserRepo.GetAllVersionChanges();
        }

        [Authorize]
        [HttpPost]
        public bool Update([FromBody]Browser browser) {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            browserRepo.Update(browser);
            return browserRepo.Save();
        }


        [Authorize]
        [HttpPost]
        public IHttpActionResult AuthTest()
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            return Ok();
        }

    }
}
