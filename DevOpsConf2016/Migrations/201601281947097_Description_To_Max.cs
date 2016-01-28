namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description_To_Max : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbstractSubmissions", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbstractSubmissions", "Description", c => c.String(nullable: false, maxLength: 750));
        }
    }
}
