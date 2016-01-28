namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAbstractSpace : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbstractSubmissions", "Description", c => c.String(nullable: false, maxLength: 750));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbstractSubmissions", "Description", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
