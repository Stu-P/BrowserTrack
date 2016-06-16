using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;
using BrowserTrack.Data.Entities;

namespace BrowserTrack.Data.EntityFramework
{
    internal sealed class BrowserTrackDBConfiguration : DbMigrationsConfiguration<BrowserTrackDBContext>
    {

        public BrowserTrackDBConfiguration()
        {
            this.AutomaticMigrationsEnabled = false;
        }




        protected override void Seed(BrowserTrackDBContext context)
        {
            base.Seed(context);

            if (context.Browsers.Count() == 0)
            {
                var chrome = new Browser
                {
                    Name = "Chrome",
                    OS = "Windows",
                    CurrentVersion = "43.12.25",
                    LastVersionCheck = DateTime.Now,
                    VersionCheckEnabled = true,
                    SearchCriteria = new SearchCriteria
                    {

                        Url = "http://en.wikipedia.org/wiki/Google_Chrome",
                        PageLocator = @"//*[@id='mw - content - text']/table[1]/tbody/tr[4]/td/p[1]/text()",
                        VersionRegex = @"(\d+[\/\d. ]*|\d)"
                    }
                };
                var IE = new Browser
                {
                    Name = "IE",
                    OS = "Windows",
                    CurrentVersion = "11.0.1",
                    LastVersionCheck = DateTime.Now,
                    VersionCheckEnabled = true,
                    SearchCriteria = new SearchCriteria
                    {

                        Url = "http://en.wikipedia.org/wiki/Internet_Explorer",
                        PageLocator = @"//*[@id='mw - content - text']/table[1]/tbody/tr[5]/td/p/text()",
                        VersionRegex = @"(\d+[\/\d. ]*|\d)"
                    }
                };
                var firefox = new Browser
                {
                    Name = "Firefox",
                    OS = "Windows",
                    CurrentVersion = "38.6521",
                    LastVersionCheck = DateTime.Now,
                    VersionCheckEnabled = true,
                    SearchCriteria = new SearchCriteria
                    {

                        Url = "http://en.wikipedia.org/wiki/Firefox",
                        PageLocator = @"//*[@id='mw-content-text']/table[1]/tbody/tr[6]/td/div/ul/li[1]/text()",
                        VersionRegex = @"(\d+[\/\d. ]*|\d)"
                    }
                };
                var safari = new Browser
                {
                    Name = "Safari",
                    OS = "OSX 10.10",
                    CurrentVersion = "8.0.7",
                    LastVersionCheck = DateTime.Now,
                    VersionCheckEnabled = true,
                    SearchCriteria = new SearchCriteria
                    {

                        Url = "http://en.wikipedia.org/wiki/Safari_(web_browser)",
                        PageLocator = @"//*[@id='mw - content - text']/table[1]/tbody/tr[5]/td/p[1]/text()",
                        VersionRegex = @"(\d+[\/\d. ]*|\d)"

                    }
                };

                if (context.VersionChanges.Count() == 0)
                {
                    var versionChanges = new List<VersionChange> {
                               new VersionChange {
                                  BrowserName = "Safari",
                                  DateOfChange = new DateTime(2015,11,6),
                                  NewVersion = "8.0.7",
                                  PriorVersion = "8.0.5"
                               },
                                new VersionChange {
                                  BrowserName = "Chrome",
                                  DateOfChange = new DateTime(2015,4,1),
                                  NewVersion = "43.2",
                                  PriorVersion = "43.1"
                               },
                               new VersionChange {
                                  BrowserName = "Safari",
                                  DateOfChange = new DateTime(2015,4,5),
                                  NewVersion = "8.0.5",
                                  PriorVersion = "8.0.2"
                               }
                        };

                    context.Browsers.Add(chrome);
                    context.Browsers.Add(firefox);
                    context.Browsers.Add(IE);
                    context.Browsers.Add(safari);

                    foreach (var v in versionChanges)
                    {
                        context.VersionChanges.Add(v);
                    }

                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
        }
    }
}