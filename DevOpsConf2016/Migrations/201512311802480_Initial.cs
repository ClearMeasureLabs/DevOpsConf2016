namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 60),
                        EMail = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        AttendeeId = c.Guid(nullable: false),
                        TwitterHandle = c.String(maxLength: 35),
                        Company = c.String(maxLength: 50),
                        CompanyURL = c.String(nullable: false, maxLength: 150),
                        BlogURL = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.AttendeeId)
                .ForeignKey("dbo.Attendees", t => t.AttendeeId)
                .Index(t => t.AttendeeId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AttendeeId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Abstract = c.String(nullable: false),
                        Objectives = c.String(),
                        Level = c.Int(nullable: false),
                        Requirements = c.String(),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Speakers", t => t.AttendeeId)
                .Index(t => t.AttendeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Speakers", "AttendeeId", "dbo.Attendees");
            DropForeignKey("dbo.Sessions", "Speaker_AttendeeId", "dbo.Speakers");
            DropForeignKey("dbo.Sessions", "SpeakerInfo_AttendeeId", "dbo.Speakers");
            DropIndex("dbo.Sessions", new[] { "Speaker_AttendeeId" });
            DropIndex("dbo.Sessions", new[] { "SpeakerInfo_AttendeeId" });
            DropIndex("dbo.Speakers", new[] { "AttendeeId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.Speakers");
            DropTable("dbo.Attendees");
        }
    }
}
