namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdIncidentVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidenteViewModels", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidenteViewModels", "ClientID");
            DropColumn("dbo.IncidenteViewModels", "OrganizationID");
        }
    }
}
