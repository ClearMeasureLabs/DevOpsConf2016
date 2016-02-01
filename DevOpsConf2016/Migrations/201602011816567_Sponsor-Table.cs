namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SponsorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sponsors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Company = c.String(nullable: false),
                        Title = c.String(),
                        Phone = c.String(nullable: false),
                        EMail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sponsors");
        }
    }
}
