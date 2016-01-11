namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Id = c.Guid(nullable: false),
                        TwitterHandle = c.String(maxLength: 35),
                        Company = c.String(maxLength: 50),
                        CompanyURL = c.String(nullable: false, maxLength: 150),
                        BlogURL = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attendees", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Abstract = c.String(nullable: false),
                        Objectives = c.String(),
                        Level = c.Int(nullable: false),
                        Requirements = c.String(),
                        Accepted = c.Boolean(nullable: false),
                        Speaker_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Speakers", t => t.Speaker_Id)
                .Index(t => t.Speaker_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Speakers", "Id", "dbo.Attendees");
            DropForeignKey("dbo.Sessions", "Speaker_Id", "dbo.Speakers");
            DropIndex("dbo.Sessions", new[] { "Speaker_Id" });
            DropIndex("dbo.Speakers", new[] { "Id" });
            DropTable("dbo.Sessions");
            DropTable("dbo.Speakers");
            DropTable("dbo.Attendees");
        }
    }
}
