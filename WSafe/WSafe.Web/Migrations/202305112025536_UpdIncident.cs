namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdIncident : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidentes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incidentes", "ClientID");
            DropColumn("dbo.Incidentes", "OrganizationID");
        }
    }
}
