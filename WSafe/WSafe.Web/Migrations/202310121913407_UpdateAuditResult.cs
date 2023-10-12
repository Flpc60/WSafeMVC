namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardAudits", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardAudits");
        }
    }
}
