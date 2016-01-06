using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Contexts
{
    public class DevOpsContext : DbContext  
    {
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
                .HasKey(e => e.Id)
                .HasOptional(e => e.SpeakerInfo)
                .WithRequired();

            modelBuilder.Entity<Speaker>()
                .HasKey(e => e.Id)
                .HasMany(e => e.Sessions)
                .WithOptional();
            modelBuilder.Entity<SessionInfo>()
                .Property(e => e.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


        }
    }
}