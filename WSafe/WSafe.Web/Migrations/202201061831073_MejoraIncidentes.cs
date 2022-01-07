namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MejoraIncidentes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IncidenteViewModels", "EquipoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncidenteViewModels", "EquipoID", c => c.Int(nullable: false));
        }
    }
}
