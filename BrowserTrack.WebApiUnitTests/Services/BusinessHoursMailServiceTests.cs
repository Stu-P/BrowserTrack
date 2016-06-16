using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserTrack.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;

namespace BrowserTrack.WebApi.Services.UnitTests
{
    [TestClass()]
    public class BusinessHoursMailServiceTests
    {

        private BusinessHoursMailService sut;
        private DateTime currentDay;

        [TestInitialize]
        public void Test_Setup() {
            sut = new BusinessHoursMailService();
            currentDay = DateTime.Today;

        }


        [TestMethod()]
        public void IfCurrentTimeWithinBusinessHoursReturnSentOk()
        {

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => currentDay.AddHours(16);

                var result = sut.SendMail(GetValidEnquiry);

                Assert.IsTrue(result == MailSentStatus.OK);
            }
        }
        [TestMethod()]
        public void IfCurrentTimeOutsideBusinessHoursReturnSentDelayed()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => currentDay.AddHours(2);
                var result = sut.SendMail(GetValidEnquiry);

                Assert.IsTrue(result == MailSentStatus.Sheduled);
            }
        }
        [TestMethod()]
        public void IfEnquiryNullReturnSentFailed()
        {
            var result = sut.SendMail(null);

            Assert.IsTrue(result == MailSentStatus.Failed);
        }
        [TestMethod()]
        public void IfFirstNameEqualsFailThenReturnSentFailed()
        {
            var result = sut.SendMail(GetInvalidEnquiry);

            Assert.IsTrue(result == MailSentStatus.Failed);
        }

        private MailEnquiry GetValidEnquiry { get {
                return new MailEnquiry
                {
                    FirstName = "stu",
                    LastName = "test",
                    Email = "stu.test@test.tst",
                    Phone = "04578445",
                    Enquiry = "hello this is a test enquiry"
                };
            }
        }

        private MailEnquiry GetInvalidEnquiry
        {
            get
            {
                return new MailEnquiry
                {
                    FirstName = "FAIL",
                    LastName = "test",
                    Email = "stu.test@test.tst",
                    Phone = "04578445",
                    Enquiry = "hello this is a test enquiry"
                };
            }
        }
    }


}