namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIncident : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncidenteViewModels", "FechaReporte", c => c.String(nullable: false));
            AlterColumn("dbo.IncidenteViewModels", "FechaIncidente", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncidenteViewModels", "FechaIncidente", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidenteViewModels", "FechaReporte", c => c.DateTime(nullable: false));
        }
    }
}
