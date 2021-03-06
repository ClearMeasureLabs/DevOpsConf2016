﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Contexts
{
    public class DevOpsContext : DbContext  
    {
        public DbSet<Login> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<AbstractSubmission> Abstracts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sponsor>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Login>()
                .HasKey(e => e.Id)
                .HasRequired(e => e.UserInfo)
                .WithRequiredDependent(x => x.UserInfo);

            modelBuilder.Entity<Attendee>()
                .HasKey(e => e.Id)
                .HasOptional(e => e.SpeakerInfo)
                .WithRequired(x => x.Attendee);

            modelBuilder.Entity<Speaker>()
                .HasKey(e => e.Id)
                .HasMany(e => e.Sessions)
                .WithOptional();
            modelBuilder.Entity<SessionInfo>()
                .Property(e => e.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AbstractSubmission>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Role>()
                .HasKey(e => e.Id)
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles);
        }
    }
}