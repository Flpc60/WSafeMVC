namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIncidenteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidentes", "CategoriasIncidente", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "EquiposInvestigador", c => c.Int(nullable: false));
            AlterColumn("dbo.Incidentes", "NaturalezaLesion", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Incidentes", "PartesAfectadas", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Incidentes", "TipoIncidente", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Incidentes", "AgenteLesion", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Incidentes", "ActosInseguros", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "CondicionesInsegura", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "Afectacion", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "DañosOcasionados", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "TipoVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.Incidentes", "MarcaVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.Incidentes", "ModeloVehiculo", c => c.String(maxLength: 20));
            AlterColumn("dbo.Incidentes", "DescripcionIncidente", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Incidentes", "EvitarIncidente", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "AccionesInmediatas", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "ComentariosAdicionales", c => c.String(maxLength: 150));
            AlterColumn("dbo.Incidentes", "AtencionBrindada", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "NaturalezaLesion", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "PartesAfectadas", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "TipoIncidente", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "AgenteLesion", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "ActosInseguros", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "CondicionesInsegura", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "DescripcionIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.IncidenteViewModels", "EvitarIncidente", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "AccionesInmediatas", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "ComentariosAdicionales", c => c.String(maxLength: 150));
            AlterColumn("dbo.IncidenteViewModels", "AtencionBrindada", c => c.String(maxLength: 150));
            DropColumn("dbo.Incidentes", "CategoriaIncidente");
            DropColumn("dbo.Incidentes", "Informante");
            DropColumn("dbo.Incidentes", "EquipoInvestigador");
            DropColumn("dbo.IncidenteViewModels", "Informante");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncidenteViewModels", "Informante", c => c.String(nullable: false));
            AddColumn("dbo.Incidentes", "EquipoInvestigador", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "Informante", c => c.String(nullable: false));
            AddColumn("dbo.Incidentes", "CategoriaIncidente", c => c.Int(nullable: false));
            AlterColumn("dbo.IncidenteViewModels", "AtencionBrindada", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "ComentariosAdicionales", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "AccionesInmediatas", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "EvitarIncidente", c => c.String(maxLength: 20));
            AlterColumn("dbo.IncidenteViewModels", "DescripcionIncidente", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "CondicionesInsegura", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "ActosInseguros", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "AgenteLesion", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "TipoIncidente", c => c.String(maxLength: 50));
            AlterColumn("dbo.IncidenteViewModels", "PartesAfectadas", c => c.String(maxLength: 100));
            AlterColumn("dbo.IncidenteViewModels", "NaturalezaLesion", c => c.String(maxLength: 100));
            AlterColumn("dbo.Incidentes", "AtencionBrindada", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "ComentariosAdicionales", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "AccionesInmediatas", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "EvitarIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "DescripcionIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "ModeloVehiculo", c => c.String());
            AlterColumn("dbo.Incidentes", "MarcaVehiculo", c => c.String());
            AlterColumn("dbo.Incidentes", "TipoVehiculo", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "DañosOcasionados", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "Afectacion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "CondicionesInsegura", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "ActosInseguros", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "AgenteLesion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "TipoIncidente", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "PartesAfectadas", c => c.String(maxLength: 250));
            AlterColumn("dbo.Incidentes", "NaturalezaLesion", c => c.String(maxLength: 250));
            DropColumn("dbo.Incidentes", "EquiposInvestigador");
            DropColumn("dbo.Incidentes", "TrabajadorID");
            DropColumn("dbo.Incidentes", "CategoriasIncidente");
        }
    }
}
