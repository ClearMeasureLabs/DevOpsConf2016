using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
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
            var role3 = new Role()
            {
                Description = "Chair",
                Id = 3
            };
            context.Roles.AddOrUpdate(x => x.Id, role3);
        }

        internal static void PopulateProduction(DevOpsContext context)
        {
            PopulateRoles(context);
            var admin = context.Roles.Find(1);
            var chair = context.Roles.Find(3);

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
                UserInfo = login,
                SpeakerInfo = speaker
            };
            context.Attendees.AddOrUpdate(x => x.Id, attendee);

            var id2 = Guid.Parse("f63c9f47-db7e-4ad0-ac2c-fc8df06491f6");

            var attendee2 = new Attendee()
            {
                Id = id2,
                FirstName = "Jeffery",
                LastName = "Palermo",
                Title = "Microsoft MVP",
                Twitter = "@jefferypalermo",
                SpeakerInfo = new Speaker()
                {
                    Id = id2,
                    Company = "Clear-Measure",
                    CompanyURL = "http://www.Clear-Measure.com",
                    BlogURL = "http://jefferypalermo.com"
                },
                UserInfo = new Login()
                {
                    Id = id2,
                    EMail ="jeffery@clear-measure.com",
                    Password = "ClearMeasure2016".EncodeToSHA1(),
                    Roles = new List<Role>() { chair}
                }

            };

            context.Attendees.AddOrUpdate(x => x.Id, attendee2);

            var id3 = Guid.Parse("f63c9f47-db7e-4ad0-ac2c-fc8df06491f7");

            var attendee3 = new Attendee()
            {
                Id = id3,
                FirstName = "Javier",
                LastName = "Lozano",
                Title = "Microsoft MVP",
                Twitter = "@jglozano",
                SpeakerInfo = new Speaker()
                {
                    Id = id3,
                    Company = "LozanoTek",
                    CompanyURL = "http://www.lozanotek.com",
                    BlogURL = "http://jglozano.io/"
                },
                UserInfo = new Login()
                {
                    Id = id3,
                    EMail = "javier@lozanotek.com",
                    Password = "LozanoTek2016".EncodeToSHA1(),
                    Roles = new List<Role>() { chair }
                }

            };

            context.Attendees.AddOrUpdate(x => x.Id, attendee3);


            var id4 = Guid.Parse("f63c9f47-db7e-4ad0-ac2c-fc8df06491f8");

            var attendee4 = new Attendee()
            {
                Id = id4,
                FirstName = "Liz",
                LastName = "Hood",
                Title = "Marketing Coordinator",
                Twitter = "",
                SpeakerInfo = new Speaker()
                {
                    Id = id4,
                    Company = "Clear-Measure",
                    CompanyURL = "http://www.Clear-Measure.com"
                },
                UserInfo = new Login()
                {
                    Id = id4,
                    EMail = "liz@clear-measure.com",
                    Password = "ClearMeasure2016".EncodeToSHA1(),
                    Roles = new List<Role>() { chair }
                }

            };
            context.Attendees.AddOrUpdate(x => x.Id, attendee4);
            context.SaveChanges();
        }
    }
}