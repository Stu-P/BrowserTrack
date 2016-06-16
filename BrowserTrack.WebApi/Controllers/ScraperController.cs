using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrowserTrack.Data.Repositories;
using BrowserTrack.Data.Entities;

namespace BrowserTrack.WebApi.Controllers
{
    public class ScraperController : ApiController
    {
        private IBrowserTrackRepository browserRepo;
        public ScraperController(IBrowserTrackRepository _browserRepo)
        {
            browserRepo = _browserRepo;
        }


        [HttpGet]
        public IQueryable<Browser> GetAllEligibleBrowserSearchDetails()
        {
            return browserRepo.GetAllEligibleBrowserSearchDetails();

        }


        // GET: api/Scraper/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Scraper
        [HttpPost]
        public bool AddVersionHistoryRecord([FromBody]VersionChange versionChange)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            return browserRepo.AddVersionChange(versionChange) && browserRepo.Save();

        }

        // PUT: api/Scraper/5
        [HttpPost]
        public bool Update([FromBody]Browser browser)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            browserRepo.Update(browser);
            return browserRepo.Save();
        }


    }
}
