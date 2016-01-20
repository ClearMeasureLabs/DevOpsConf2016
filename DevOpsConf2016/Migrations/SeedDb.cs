using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DevOpsConf2016.Extensions;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Migrations
{
    public class SeedDb
    {
        public static void Populate(DevOpsConf2016.Contexts.DevOpsContext context)
        {
            var id = Guid.Parse("e63c9f47-db7e-4ad0-ac2c-fc8df06491f5");
            var login = new Login()
            {
                Id = id,
                EMail = "who@where.com",
                Password = "test".EncodeToSHA1()
            };

            var attendee = new Attendee()
            {
                Id = id,
                FirstName = "Who",
                LastName = "Villian",
                Title = "Sr. Whovener",
                UserInfo = login
            };

            var speaker =
                new Speaker()
                {
                    Id = id,
                    BlogURL = "http://who.com",
                    Company = "WhoACME",
                    CompanyURL = "http://who.com",
                    TwitterHandle = "@whovian",
                    Attendee = attendee
                };
            var session = new SessionInfo()
            {
                Abstract = "This is the abstract",
                Accepted = false,
                Level = TalkLevel.Advanced,
                Objectives = "Objectives",
                Requirements = "test",
                Title = "test"
            };
            var sessions = new List<SessionInfo>();
            sessions.Add(session);
            speaker.Sessions = sessions;
            attendee.SpeakerInfo = speaker;
            context.Attendees.AddOrUpdate(x => x.Id, attendee);


            id = Guid.Parse("235ff3e5-8abe-4ef5-95b1-8fb8f2a4ddc5");
            var secondLogin = new Login()
            {
                Id = id,
                EMail = "prez@whitehouse.gov",
                Password = "test".EncodeToSHA1()
            };
            var secondAttendee = new Attendee()
            {
                Id = secondLogin.Id,
                FirstName = "Who",
                LastName = "Attendee",
                Title = "Analyst",
                UserInfo = secondLogin
            };

            context.Attendees.Add(secondAttendee);
            context.SaveChanges();


        }

    }
}