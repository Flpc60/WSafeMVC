namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIncidenteVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidenteViewModels", "FechaIncidentStr", c => c.String(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "FechaReportStr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidenteViewModels", "FechaReportStr");
            DropColumn("dbo.IncidenteViewModels", "FechaIncidentStr");
        }
    }
}
