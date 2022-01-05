namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createIncidenteVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incidentes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaReporte = c.DateTime(nullable: false),
                        FechaIncidente = c.DateTime(nullable: false),
                        CategoriaIncidente = c.Int(nullable: false),
                        IncapacidadMedica = c.Boolean(nullable: false),
                        DiasIncapacidad = c.Int(nullable: false),
                        NaturalezaLesion = c.String(),
                        PartesAfectadas = c.String(),
                        TipoIncidente = c.String(),
                        AgenteLesion = c.String(),
                        ActosInseguros = c.String(),
                        CondicionesInsegura = c.String(),
                        TipoDaño = c.String(),
                        Afectacion = c.String(),
                        DañosOcasionados = c.String(),
                        TipoVehiculo = c.String(),
                        MarcaVehiculo = c.String(),
                        ModeloVehiculo = c.String(),
                        KilometrajeVehiculo = c.Int(nullable: false),
                        CostosEstimados = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DescripcionIncidente = c.String(),
                        EvitarIncidente = c.String(),
                        AccionesInmediatas = c.String(),
                        ComentariosAdicionales = c.String(),
                        AtencionBrindada = c.String(),
                        EquipoInvestigador = c.Int(nullable: false),
                        LesionPersonal = c.Int(nullable: false),
                        DañoMaterial = c.Int(nullable: false),
                        MedioAmbiente = c.Int(nullable: false),
                        Imagen = c.Int(nullable: false),
                        RequiereInvestigacion = c.Boolean(nullable: false),
                        ConsecuenciasLesion = c.Int(nullable: false),
                        ConsecuenciasDaño = c.Int(nullable: false),
                        ConsecuenciasMedio = c.Int(nullable: false),
                        ConsecuenciasImagen = c.Int(nullable: false),
                        Probabilidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IncidenteViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaReporte = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        FechaIncidente = c.DateTime(nullable: false),
                        CategoriasIncidente = c.Int(nullable: false),
                        IncapacidadMedica = c.Boolean(nullable: false),
                        DiasIncapacidad = c.Int(nullable: false),
                        NaturalezaLesion = c.String(),
                        PartesAfectadas = c.String(),
                        TipoIncidente = c.String(),
                        AgenteLesion = c.String(),
                        ActosInseguros = c.String(),
                        CondicionesInsegura = c.String(),
                        TipoDaño = c.String(),
                        Afectacion = c.String(),
                        DañosOcasionados = c.String(),
                        TipoVehiculo = c.String(),
                        MarcaVehiculo = c.String(),
                        ModeloVehiculo = c.String(),
                        KilometrajeVehiculo = c.Int(nullable: false),
                        CostosEstimados = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DescripcionIncidente = c.String(),
                        EvitarIncidente = c.String(),
                        AccionesInmediatas = c.String(),
                        ComentariosAdicionales = c.String(),
                        AtencionBrindada = c.String(),
                        EquipoID = c.Int(nullable: false),
                        EquiposInvestigador = c.Int(nullable: false),
                        LesionPersonal = c.Int(nullable: false),
                        DañoMaterial = c.Int(nullable: false),
                        MedioAmbiente = c.Int(nullable: false),
                        Imagen = c.Int(nullable: false),
                        RequiereInvestigacion = c.Boolean(nullable: false),
                        ConsecuenciasLesion = c.Int(nullable: false),
                        ConsecuenciasDaño = c.Int(nullable: false),
                        ConsecuenciasMedio = c.Int(nullable: false),
                        ConsecuenciasImagen = c.Int(nullable: false),
                        Probabilidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Accions", "CausaAccion", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "SubCausa", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "UltraCausa", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Nombres", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaNomina", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "DiasPago", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "TipoVinculacion", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "EPS", c => c.String());
            AddColumn("dbo.Trabajadors", "AFP", c => c.String());
            AddColumn("dbo.Trabajadors", "ARL", c => c.String());
            AddColumn("dbo.AccionViewModels", "Prioritaria", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accions", "CausasAccion");
            DropColumn("dbo.Accions", "FechaInicial");
            DropColumn("dbo.Accions", "FechaFinal");
            DropColumn("dbo.Accions", "Plan");
            DropColumn("dbo.Accions", "Seguimiento");
            DropColumn("dbo.Accions", "FechaSeguimiento");
            DropColumn("dbo.Accions", "FechaCierre");
            DropColumn("dbo.Accions", "Efectividad");
            DropColumn("dbo.Trabajadors", "PrimerNombre");
            DropColumn("dbo.Trabajadors", "SegundoNombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "SegundoNombre", c => c.String());
            AddColumn("dbo.Trabajadors", "PrimerNombre", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Efectividad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accions", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "FechaSeguimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "Seguimiento", c => c.String());
            AddColumn("dbo.Accions", "Plan", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "FechaFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "FechaInicial", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "CausasAccion", c => c.Int(nullable: false));
            DropColumn("dbo.AccionViewModels", "Prioritaria");
            DropColumn("dbo.Trabajadors", "ARL");
            DropColumn("dbo.Trabajadors", "AFP");
            DropColumn("dbo.Trabajadors", "EPS");
            DropColumn("dbo.Trabajadors", "TipoVinculacion");
            DropColumn("dbo.Trabajadors", "DiasPago");
            DropColumn("dbo.Trabajadors", "FechaNomina");
            DropColumn("dbo.Trabajadors", "Nombres");
            DropColumn("dbo.Accions", "UltraCausa");
            DropColumn("dbo.Accions", "SubCausa");
            DropColumn("dbo.Accions", "CausaAccion");
            DropTable("dbo.IncidenteViewModels");
            DropTable("dbo.Incidentes");
        }
    }
}
