namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendees", "Title", c => c.String(maxLength: 60));
            AlterColumn("dbo.Attendees", "Twitter", c => c.String(maxLength: 30));
            AlterColumn("dbo.Speakers", "CompanyURL", c => c.String(maxLength: 150));
            AlterColumn("dbo.Speakers", "BlogURL", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Speakers", "BlogURL", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Speakers", "CompanyURL", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Attendees", "Twitter", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Attendees", "Title", c => c.String(nullable: false, maxLength: 60));
        }
    }
}
