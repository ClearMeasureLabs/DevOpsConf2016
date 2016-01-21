namespace DevOpsConf2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistrationAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleLogins",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Login_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Login_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Login_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Login_Id);
            
            DropColumn("dbo.Speakers", "TwitterHandle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Speakers", "TwitterHandle", c => c.String(maxLength: 35));
            DropForeignKey("dbo.RoleLogins", "Login_Id", "dbo.Users");
            DropForeignKey("dbo.RoleLogins", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RoleLogins", new[] { "Login_Id" });
            DropIndex("dbo.RoleLogins", new[] { "Role_Id" });
            DropTable("dbo.RoleLogins");
            DropTable("dbo.Roles");
        }
    }
}
