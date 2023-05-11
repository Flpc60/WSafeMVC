namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdRisk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Riesgoes", "ClientID");
            DropColumn("dbo.Riesgoes", "OrganizationID");
        }
    }
}
