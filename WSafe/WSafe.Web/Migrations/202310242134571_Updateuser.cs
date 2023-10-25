namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditedCreateVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditedCreateVMs", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditedCreateVMs", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Audits", "AuditDocument", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Audits", "AuditDocument", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Users", "RegisterDate");
            DropColumn("dbo.AuditedCreateVMs", "UserID");
            DropColumn("dbo.AuditedCreateVMs", "ClientID");
            DropColumn("dbo.AuditedCreateVMs", "OrganizationID");
        }
    }
}
