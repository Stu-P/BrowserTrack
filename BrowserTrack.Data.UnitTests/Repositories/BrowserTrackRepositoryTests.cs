using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserTrack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserTrack.Data.UnitTests;
using Ninject;
using Ninject.Modules;
using Moq;
using BrowserTrack.Data.EntityFramework;
using BrowserTrack.Data.Entities;
using System.Data.Entity;

namespace BrowserTrack.Data.Repositories.UnitTests
{
    [TestClass()]
    public class BrowserTrackRepositoryTests : TestBase
    {
        private BrowserTrackRepository sut;

        public Mock<BrowserTrackDBContext> browserTrackStub { get; set; }

        private Browser expectedBrowser;
        private Browser disabledBrowser;

        [TestInitialize]
        public void Test_Setup()
        {
            expectedBrowser = new Browser()
            {
                Id = 5,
                Name = "Chrome",
                OS = "windows",
                CurrentVersion = "42.1",
                LastVersionCheck = new DateTime(2016, 12, 11),
                VersionCheckEnabled = true,
                SearchCriteria = new SearchCriteria()
                {
                    Id = 25,
                    Url = "http://en.wikipedia.org/wiki/Google_Chrome",
                    PageLocator = "//*[@id='mw - content - text']/table[1]/tbody/tr[4]/td/p[1]/text()",
                    VersionRegex = "(\\d+[\\/\\d. ]*|\\d)"
                }
            };


            disabledBrowser = new Browser()
            {
                Id = 6,
                Name = "Firefox",
                OS = "windows",
                CurrentVersion = "42.1",
                LastVersionCheck = new DateTime(2015, 12, 11),
                VersionCheckEnabled = false

            };


            var browserData = new List<Browser> { expectedBrowser, disabledBrowser }.AsQueryable();

            var historyData = new List<VersionChange> {
                new VersionChange() { Id=1, BrowserName = "Chrome", DateOfChange = new DateTime(2016,6,6), PriorVersion = "45.4", NewVersion="46.212" },
                new VersionChange() { Id = 2, BrowserName = "IE", DateOfChange = new DateTime(2016,4,23), PriorVersion = "101.412", NewVersion="201.476" }
            }.AsQueryable();



            var browserDbSetMock = new Mock<IDbSet<Browser>>();
            var historyDbSetMock = new Mock<IDbSet<VersionChange>>();

            browserDbSetMock.Setup(m => m.Provider).Returns(browserData.Provider);
            browserDbSetMock.Setup(m => m.Expression).Returns(browserData.Expression);
            browserDbSetMock.Setup(m => m.ElementType).Returns(browserData.ElementType);
            browserDbSetMock.Setup(m => m.GetEnumerator()).Returns(browserData.GetEnumerator());

            historyDbSetMock.Setup(m => m.Provider).Returns(historyData.Provider);
            historyDbSetMock.Setup(m => m.Expression).Returns(historyData.Expression);
            historyDbSetMock.Setup(m => m.ElementType).Returns(historyData.ElementType);
            historyDbSetMock.Setup(m => m.GetEnumerator()).Returns(historyData.GetEnumerator());
            

            browserTrackStub = new Mock<BrowserTrackDBContext>();
            browserTrackStub.Setup(x => x.Browsers).Returns(browserDbSetMock.Object);
            browserTrackStub.Setup(x => x.VersionChanges).Returns(historyDbSetMock.Object);

            sut = new BrowserTrackRepository(browserTrackStub.Object);

        }


        [TestMethod()]
        public void RequestingAllBrowsersReturnsExpectedBrowser()
        {
            //Setup

            //Act
            var browsers = sut.GetAllBrowsers().ToList();

            //Assert

            Assert.IsNotNull(browsers);
            CollectionAssert.Contains(browsers, expectedBrowser);

            browserTrackStub.Verify(x => x.Browsers, Times.Exactly(1));

        }

        [TestMethod()]
        public void RequestingBrowsersReturnsAllBrowsersIncludingInactive()
        {
            //Setup

            //Act
            var browsers = sut.GetAllBrowsers().ToList();

            //Assert
            Assert.AreEqual(2, browsers.Count);
            CollectionAssert.Contains(browsers, disabledBrowser);
            browserTrackStub.Verify(x => x.Browsers, Times.Exactly(1));
        }


        [TestMethod()]
        public void RequestingBrowserSearchDetailsIncludesSearchDetails()
        {
            //Setup

            //Act
            var browser = sut.GetBrowserSearchDetails(5);

            //Assert
            Assert.IsNotNull(browser);
            Assert.IsInstanceOfType(browser, typeof(Browser));
            browserTrackStub.Verify(x => x.Browsers, Times.Exactly(1));
            Assert.IsNotNull(browser.SearchCriteria);

        }


        [TestMethod()]
        public void GetAllEligibleBrowserSearchDetailsReturnsOnlyBrowsersEnabledForSearching()
        {
            //Setup

            //Act
            var browsers = sut.GetAllEligibleBrowserSearchDetails().ToList();

            //Assert

            Assert.IsNotNull(browsers);
            Assert.AreEqual(1, browsers.Count);
            CollectionAssert.Contains(browsers, expectedBrowser);
            CollectionAssert.DoesNotContain(browsers, disabledBrowser);
            browserTrackStub.Verify(x => x.Browsers, Times.Exactly(1));

        }





        [TestMethod()]
        public void ChangeEnabledStateToTrueWillEnableVersionCheckForSpecifiedBrowser()
        {
            //Setup

            //Act
            sut.ChangeEnabledState(6, true);

            var actual = sut.GetAllBrowsers().First(x => x.Id == 6);
            //Assert
            Assert.AreEqual(true, actual.VersionCheckEnabled);

        }

        [TestMethod()]
        public void ChangeEnabledStateToFalseWillDisableVersionCheckForSpecifiedBrowser()
        {
            //Setup

            //Act
            sut.ChangeEnabledState(5, false);
            var actual = sut.GetAllBrowsers().First(x => x.Id == 5);

            //Assert
            Assert.AreEqual(false, actual.VersionCheckEnabled);

        }

        [TestMethod()]
        public void AttemptingToEnableVersionCheckOnBrowserWhichIsAlreadyEnabledWillNotAlterState()
        {
            //Setup

            //Act
            sut.ChangeEnabledState(5, true);
            var actual = sut.GetAllBrowsers().First(x => x.Id == 5);
            //Assert
            Assert.AreEqual(true, actual.VersionCheckEnabled);

        }



        [TestMethod()]
        public void GetAllVersionChangesReturnsExpectedResult()
        {
            //Setup

            //Act
            var history = sut.GetAllVersionChanges().ToList(); 
            //Assert
            Assert.IsNotNull(history);
            browserTrackStub.Verify(x => x.VersionChanges, Times.Exactly(1));
            Assert.AreEqual(2, history.Count);

        }
        [TestMethod()]
        public void CanAddNewVersionChangeToHistory()
        {
            //Setup
            var newItem = new VersionChange() { Id = 3, BrowserName = "Chrome", DateOfChange = DateTime.Now, PriorVersion = "1.1", NewVersion = "2.2" };

            //Act
           bool result = sut.AddVersionChange(newItem);
            sut.Save();

            //Assert
            Assert.IsTrue(result);
            browserTrackStub.Verify(x => x.VersionChanges, Times.Exactly(1));


        }







    }
}