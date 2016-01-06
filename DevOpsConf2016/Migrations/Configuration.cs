using DevOpsConf2016.Models;

namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DevOpsConf2016.Contexts.DevOpsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DevOpsConf2016.Contexts.DevOpsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var id = Guid.NewGuid();

            context.Attendees.AddOrUpdate(
                p => p.Id,
                new Attendee() { EMail = "who@where.com", FirstName = "Who", LastName = "Villian", Id = id, Title = "Sr. Whovener" }
                );
            context.Speakers.AddOrUpdate(
                p => p.AttendeeId,
                new Speaker() { AttendeeId = id, BlogURL = "http://who.com", Company = "WhoACME", CompanyURL = "http://who.com", TwitterHandle = "@whovian" }
                );
            context.Sessions.AddOrUpdate(
                p => p.ID,
                new SessionInfo() { AttendeeId = id, Abstract = "This is the abstract", Accepted = false, Level = TalkLevel.Advanced, Objectives = "Objectives", Requirements = "", Title = "" }
                );
        }
    }
}
