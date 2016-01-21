using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Extensions;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Migrations
{
    public class SeedDb
    {
        public static void Populate(DevOpsConf2016.Contexts.DevOpsContext context)
        {
            PopulateRoles(context);

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
                Twitter = "@whovian",
                UserInfo = login
            };

            var speaker =
                new Speaker()
                {
                    Id = id,
                    BlogURL = "http://who.com",
                    Company = "WhoACME",
                    CompanyURL = "http://who.com",
                    
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

        private static void PopulateRoles(DevOpsContext context)
        {
            var role = new Role()
            {
                Description = "Admin",
                Id = 1
            };

            context.Roles.AddOrUpdate(x => x.Id, role);

            var role2 = new Role()
            {
                Description = "User",
                Id = 2
            };
            context.Roles.AddOrUpdate(x => x.Id, role2);
        }

        internal static void PopulateProduction(DevOpsContext context)
        {
            PopulateRoles(context);
            var admin = context.Roles.Find(1);

            var id = Guid.Parse("e63c9f47-db7e-4ad0-ac2c-fc8df06491f5");
            var login = new Login()
            {
                EMail = "gus@clear-measure.com",
                Id = id,
                Password = "ClearMeasure2016".EncodeToSHA1(),
                Roles = new List<Role>() {admin},
            };

            var speaker = new Speaker()
            {
                Id = id,
                BlogURL = "http://blog.kosmikinnovations.com",
                Company = "Clear-Measure",
                CompanyURL = "http://www.clear-measure.com"
            };
            var attendee = new Attendee()
            {
                Id = id,
                FirstName = "Gus",
                LastName = "Emery",
                Title = "Principal Architect",
                Twitter = "@n_f_e",
                UserInfo = login
            };
            context.Attendees.AddOrUpdate(x => x.Id, attendee);
            context.SaveChanges();
        }
    }
}