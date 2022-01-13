namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AplicateDataAnnotation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Incidentes", "NaturalezaLesion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "PartesAfectadas", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "TipoIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "AgenteLesion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "ActosInseguros", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "CondicionesInsegura", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "TipoDaño", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "Afectacion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "DañosOcasionados", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "TipoVehiculo", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "DescripcionIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "EvitarIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "AccionesInmediatas", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "ComentariosAdicionales", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "AtencionBrindada", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Incidentes", "AtencionBrindada", c => c.String());
            AlterColumn("dbo.Incidentes", "ComentariosAdicionales", c => c.String());
            AlterColumn("dbo.Incidentes", "AccionesInmediatas", c => c.String());
            AlterColumn("dbo.Incidentes", "EvitarIncidente", c => c.String());
            AlterColumn("dbo.Incidentes", "DescripcionIncidente", c => c.String());
            AlterColumn("dbo.Incidentes", "TipoVehiculo", c => c.String());
            AlterColumn("dbo.Incidentes", "DañosOcasionados", c => c.String());
            AlterColumn("dbo.Incidentes", "Afectacion", c => c.String());
            AlterColumn("dbo.Incidentes", "TipoDaño", c => c.String());
            AlterColumn("dbo.Incidentes", "CondicionesInsegura", c => c.String());
            AlterColumn("dbo.Incidentes", "ActosInseguros", c => c.String());
            AlterColumn("dbo.Incidentes", "AgenteLesion", c => c.String());
            AlterColumn("dbo.Incidentes", "TipoIncidente", c => c.String());
            AlterColumn("dbo.Incidentes", "PartesAfectadas", c => c.String());
            AlterColumn("dbo.Incidentes", "NaturalezaLesion", c => c.String());
        }
    }
}
