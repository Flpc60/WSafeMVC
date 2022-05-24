namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccionVM1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AplicacionVMs", "Nombre", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AplicacionVMs", "Beneficios", c => c.String(maxLength: 100));
            AlterColumn("dbo.AplicacionVMs", "Observaciones", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "NaturalezaLesion", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "PartesAfectadas", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "TipoIncidente", c => c.String(maxLength: 50));
            AlterColumn("dbo.IncidenteViewModels", "AgenteLesion", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "ActosInseguros", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "CondicionesInsegura", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "TipoDaño", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "Afectacion", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "DañosOcasionados", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "TipoVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "MarcaVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "ModeloVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "DescripcionIncidente", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "EvitarIncidente", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "AccionesInmediatas", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "ComentariosAdicionales", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "AtencionBrindada", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncidenteViewModels", "AtencionBrindada", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "ComentariosAdicionales", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "AccionesInmediatas", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "EvitarIncidente", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "DescripcionIncidente", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "ModeloVehiculo", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "MarcaVehiculo", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "TipoVehiculo", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "DañosOcasionados", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "Afectacion", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "TipoDaño", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "CondicionesInsegura", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "ActosInseguros", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "AgenteLesion", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "TipoIncidente", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "PartesAfectadas", c => c.String());
            AlterColumn("dbo.IncidenteViewModels", "NaturalezaLesion", c => c.String());
            AlterColumn("dbo.AplicacionVMs", "Observaciones", c => c.String(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "Beneficios", c => c.String());
            AlterColumn("dbo.AplicacionVMs", "Nombre", c => c.String(nullable: false));
        }
    }
}
