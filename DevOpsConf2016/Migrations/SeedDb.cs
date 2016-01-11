using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Migrations
{
    public class SeedDb
    {
        public static void Populate(DevOpsConf2016.Contexts.DevOpsContext context)
        {
            var id = Guid.NewGuid();

            var attendee = new Attendee()
            {
                Id = id,
                EMail = "who@where.com",
                FirstName = "Who",
                LastName = "Villian",
                Title = "Sr. Whovener"
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
            context.Attendees.Add(attendee);

            var secondAttendee = new Attendee()
            {
                Id = Guid.NewGuid(),
                EMail = "who@where.com",
                FirstName = "Who",
                LastName = "Attendee",
                Title = "Analyst"
,
            };

            context.Attendees.Add(secondAttendee);
            context.SaveChanges();


        }

    }
}