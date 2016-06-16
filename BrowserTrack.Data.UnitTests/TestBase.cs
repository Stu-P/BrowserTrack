using BrowserTrack.Data.EntityFramework;
using BrowserTrack.Data.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTrack.Data.UnitTests
{
    public class TestBase
    {
        protected StandardKernel kernel { get; set; }

        public TestBase() {
            kernel = new StandardKernel();

        }

        private static void RegisterTypes(IKernel kernel) {
            kernel.Bind<IBrowserTrackRepository>().To<BrowserTrackRepository>();
            kernel.Bind<BrowserTrackDBContext>().To<BrowserTrackDBContext>();
        }
    }
}
