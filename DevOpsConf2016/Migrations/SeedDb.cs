﻿using System;
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
            var id = Guid.NewGuid();
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
            context.Attendees.Add(attendee);

            var secondAttendee = new Attendee()
            {
                Id = Guid.NewGuid(),
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