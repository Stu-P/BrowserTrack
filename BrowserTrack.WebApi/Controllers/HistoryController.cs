using BrowserTrack.Data.Entities;
using BrowserTrack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BrowserTrack.WebApi.Controllers
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HistoryController : ApiController
    {
        private IBrowserTrackRepository browserRepo;

        public HistoryController(IBrowserTrackRepository _browserRepo)
        {
            browserRepo = _browserRepo;
        }

        [HttpGet]
        public IQueryable<VersionChange> GetAllVersionChanges()
        {
            return browserRepo.GetAllVersionChanges();
        }
    }
}
