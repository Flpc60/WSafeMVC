namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncidentesCreationsNew1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IncidenteViewModels", "ZonaID");
            DropColumn("dbo.IncidenteViewModels", "ProcesoID");
            DropColumn("dbo.IncidenteViewModels", "TareaID");
            DropColumn("dbo.IncidenteViewModels", "TrabajadorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncidenteViewModels", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "ZonaID", c => c.Int(nullable: false));
        }
    }
}
