namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmpresaDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes");
            DropForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads");
            DropForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls");
            DropForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Riesgoes", "Peligro_ID", "dbo.Peligroes");
            DropForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias");
            DropForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes");
            DropForeignKey("dbo.Riesgoes", "Tarea_ID", "dbo.Tareas");
            DropForeignKey("dbo.Riesgoes", "Zona_ID", "dbo.Zonas");
            DropIndex("dbo.Riesgoes", new[] { "Actividad_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Peligro_ID" });
            DropIndex("dbo.Riesgoes", new[] { "PeorConsecuencia_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Proceso_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Tarea_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Zona_ID" });
            DropIndex("dbo.Accions", new[] { "RiesgoID" });
            DropIndex("dbo.Accions", new[] { "Trabajador_ID" });
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropIndex("dbo.Aplicacions", new[] { "Control_ID" });
            DropIndex("dbo.Aplicacions", new[] { "Trabajador_ID" });
            CreateTable(
                "dbo.Accidentadoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidenteID = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Incidentes", t => t.IncidenteID, cascadeDelete: true)
                .Index(t => t.IncidenteID);
            
            CreateTable(
                "dbo.AccionViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        Categoria = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        FuenteAccion = c.Int(nullable: false),
                        Origen = c.String(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 100),
                        EficaciaAntes = c.Int(nullable: false),
                        EficaciaDespues = c.Int(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        Efectiva = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        FechaSolicitudStr = c.String(),
                        FechaCierreStr = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlanActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Causa = c.Int(nullable: false),
                        Accion = c.String(nullable: false, maxLength: 100),
                        TrabajadorID = c.Int(nullable: false),
                        Prioritaria = c.Boolean(nullable: false),
                        Costos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Responsable = c.String(maxLength: 100),
                        AccionViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccionViewModels", t => t.AccionViewModel_ID)
                .Index(t => t.AccionViewModel_ID);
            
            CreateTable(
                "dbo.Seguimientoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        Resultado = c.String(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                        AccionViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccionViewModels", t => t.AccionViewModel_ID)
                .Index(t => t.AccionViewModel_ID);
            
            CreateTable(
                "dbo.AplicacionVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        CategoriaAplicacion = c.Int(nullable: false),
                        Intervencion = c.Int(nullable: false),
                        Beneficios = c.String(maxLength: 100),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Observaciones = c.String(nullable: false, maxLength: 100),
                        TextFechaInicial = c.String(),
                        TextFechaFinal = c.String(),
                        TextCategoria = c.String(),
                        TextIntervencion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AuditaAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Usuario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AuditRiesgoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Usuario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CreateIndicatorsViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        IndicadorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Formato = c.String(),
                        Estandar = c.String(),
                        Titulo = c.String(),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Incidentes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaReporte = c.DateTime(nullable: false),
                        FechaIncidente = c.DateTime(nullable: false),
                        CategoriasIncidente = c.Int(nullable: false),
                        IncapacidadMedica = c.Boolean(nullable: false),
                        DiasIncapacidad = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        NaturalezaLesion = c.String(nullable: false, maxLength: 150),
                        PartesAfectadas = c.String(nullable: false, maxLength: 150),
                        TipoIncidente = c.String(nullable: false, maxLength: 150),
                        AgenteLesion = c.String(nullable: false, maxLength: 150),
                        ActosInseguros = c.String(maxLength: 150),
                        CondicionesInsegura = c.String(maxLength: 150),
                        TipoDaño = c.String(maxLength: 250),
                        Afectacion = c.String(maxLength: 150),
                        DañosOcasionados = c.String(maxLength: 150),
                        TipoVehiculo = c.String(maxLength: 20),
                        MarcaVehiculo = c.String(maxLength: 20),
                        ModeloVehiculo = c.String(maxLength: 20),
                        KilometrajeVehiculo = c.Int(nullable: false),
                        CostosEstimados = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DescripcionIncidente = c.String(nullable: false, maxLength: 250),
                        EvitarIncidente = c.String(maxLength: 150),
                        AccionesInmediatas = c.String(maxLength: 150),
                        ComentariosAdicionales = c.String(maxLength: 150),
                        AtencionBrindada = c.String(nullable: false, maxLength: 150),
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
                        RiesgoID = c.Int(nullable: false),
                        AccionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IncidenteViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaReporte = c.DateTime(nullable: false),
                        FechaIncidente = c.DateTime(nullable: false),
                        CategoriasIncidente = c.Int(nullable: false),
                        IncapacidadMedica = c.Boolean(nullable: false),
                        DiasIncapacidad = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        NaturalezaLesion = c.String(nullable: false, maxLength: 150),
                        PartesAfectadas = c.String(maxLength: 150),
                        TipoIncidente = c.String(nullable: false, maxLength: 150),
                        AgenteLesion = c.String(nullable: false, maxLength: 150),
                        ActosInseguros = c.String(maxLength: 150),
                        CondicionesInsegura = c.String(maxLength: 150),
                        TipoDaño = c.String(maxLength: 100),
                        Afectacion = c.String(maxLength: 100),
                        DañosOcasionados = c.String(maxLength: 100),
                        TipoVehiculo = c.String(maxLength: 20),
                        MarcaVehiculo = c.String(maxLength: 20),
                        ModeloVehiculo = c.String(maxLength: 20),
                        KilometrajeVehiculo = c.Int(nullable: false),
                        CostosEstimados = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DescripcionIncidente = c.String(maxLength: 250),
                        EvitarIncidente = c.String(maxLength: 150),
                        AccionesInmediatas = c.String(maxLength: 150),
                        ComentariosAdicionales = c.String(maxLength: 150),
                        AtencionBrindada = c.String(maxLength: 150),
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
                        FechaIncidentStr = c.String(),
                        FechaReportStr = c.String(),
                        Probabilidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Indicadors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Definicion = c.String(nullable: false),
                        Numerador = c.String(nullable: false),
                        Denominador = c.String(nullable: false),
                        Formula = c.String(nullable: false),
                        Interpretacion = c.String(nullable: false),
                        Periodicidad = c.String(nullable: false),
                        TipoChart = c.String(),
                        Meta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fuente = c.String(),
                        Tipo = c.String(),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IndicadorViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Periodo = c.String(),
                        Nombre = c.String(nullable: false),
                        Definicion = c.String(nullable: false),
                        Numerador = c.String(nullable: false),
                        Denominador = c.String(nullable: false),
                        Formula = c.String(nullable: false),
                        Interpretacion = c.String(nullable: false),
                        Periodicidad = c.String(nullable: false),
                        Imagen = c.String(),
                        Titulo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NIT = c.String(nullable: false),
                        RazonSocial = c.String(nullable: false, maxLength: 50),
                        Direccion = c.String(nullable: false, maxLength: 50),
                        Municip = c.String(nullable: false, maxLength: 50),
                        Department = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        ARL = c.String(nullable: false, maxLength: 50),
                        ClaseRiesgo = c.String(nullable: false, maxLength: 10),
                        DocumentRepresent = c.String(nullable: false, maxLength: 20),
                        NameRepresent = c.String(nullable: false, maxLength: 50),
                        EconomicActivity = c.String(nullable: false, maxLength: 100),
                        NumeroTrabajadores = c.Int(nullable: false),
                        Products = c.String(nullable: false, maxLength: 150),
                        Mision = c.String(nullable: false, maxLength: 150),
                        Vision = c.String(nullable: false, maxLength: 150),
                        Objetivos = c.String(nullable: false, maxLength: 150),
                        Procesos = c.String(maxLength: 150),
                        Organigrama = c.String(maxLength: 150),
                        TurnosAdministrativo = c.String(maxLength: 150),
                        TurnosOperativo = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlanAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Causa = c.Int(nullable: false),
                        Accion = c.String(nullable: false, maxLength: 100),
                        TrabajadorID = c.Int(nullable: false),
                        Prioritaria = c.Boolean(nullable: false),
                        Costos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Responsable = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RiesgoViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        Rutinaria = c.Boolean(nullable: false),
                        CategoriaPeligroID = c.Int(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        EfectosPosibles = c.Int(nullable: false),
                        NivelDeficiencia = c.Int(nullable: false),
                        NivelesDeficiencia = c.Int(nullable: false),
                        NivelExposicion = c.Int(nullable: false),
                        NivelesExposicion = c.Int(nullable: false),
                        NivelConsecuencia = c.Int(nullable: false),
                        NivelesConsecuencia = c.Int(nullable: false),
                        AceptabilidadNR = c.Int(nullable: false),
                        NroExpuestos = c.Int(nullable: false),
                        RequisitoLegal = c.Boolean(nullable: false),
                        IncidenteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleUserVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SeguimientoAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        Resultado = c.String(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Role = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Riesgoes", "ZonaID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "PeligroID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "PeorConsecuencia", c => c.String());
            AddColumn("dbo.Riesgoes", "IncidenteID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "AccionID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ZonaID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "FuenteAccion", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "Descripcion", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Accions", "EficaciaAntes", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "EficaciaDespues", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "Efectiva", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accions", "Estado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trabajadors", "Nombres", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaNomina", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "DiasPago", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "CargoID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Nombre", c => c.String());
            AddColumn("dbo.Aplicacions", "Finalidad", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Intervencion", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Beneficios", c => c.String());
            AddColumn("dbo.Aplicacions", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "AplicacionID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "CategoriaAplicacion", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "CategoriaEfectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Trazas", "AplicacionID", c => c.Int(nullable: false));
            AddColumn("dbo.Trazas", "RiesgoolID", c => c.Int(nullable: false));
            AlterColumn("dbo.Trabajadors", "Foto", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "Firma", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "Direccion", c => c.String(maxLength: 50));
            AlterColumn("dbo.Aplicacions", "Observaciones", c => c.String(nullable: false));
            DropColumn("dbo.Riesgoes", "Actividad_ID");
            DropColumn("dbo.Riesgoes", "Peligro_ID");
            DropColumn("dbo.Riesgoes", "PeorConsecuencia_ID");
            DropColumn("dbo.Riesgoes", "Proceso_ID");
            DropColumn("dbo.Riesgoes", "Tarea_ID");
            DropColumn("dbo.Riesgoes", "Zona_ID");
            DropColumn("dbo.Accions", "Intervenciones");
            DropColumn("dbo.Accions", "Observaciones");
            DropColumn("dbo.Accions", "FechaInicial");
            DropColumn("dbo.Accions", "FechaFinal");
            DropColumn("dbo.Accions", "MedidasControl");
            DropColumn("dbo.Accions", "Trabajador_ID");
            DropColumn("dbo.Trabajadors", "PrimerNombre");
            DropColumn("dbo.Trabajadors", "SegundoNombre");
            DropColumn("dbo.Trabajadors", "Email");
            DropColumn("dbo.Trabajadors", "FechaIngreso");
            DropColumn("dbo.Trabajadors", "Telefonos");
            DropColumn("dbo.Trabajadors", "AltoRiesgo");
            DropColumn("dbo.Trabajadors", "ActividadAltoRiesgo");
            DropColumn("dbo.Trabajadors", "Cargo_ID");
            DropColumn("dbo.Aplicacions", "CategoriaEfectividad");
            DropColumn("dbo.Aplicacions", "Control_ID");
            DropColumn("dbo.Aplicacions", "Trabajador_ID");
            DropColumn("dbo.Trazas", "RiesgoID");
            DropTable("dbo.Consecuencias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Consecuencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NivelConsecuencia = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Trazas", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Trabajador_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Control_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "CategoriaEfectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "ActividadAltoRiesgo", c => c.String());
            AddColumn("dbo.Trabajadors", "AltoRiesgo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trabajadors", "Telefonos", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaIngreso", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "Email", c => c.String());
            AddColumn("dbo.Trabajadors", "SegundoNombre", c => c.String());
            AddColumn("dbo.Trabajadors", "PrimerNombre", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Trabajador_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "MedidasControl", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "FechaFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "FechaInicial", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "Observaciones", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Intervenciones", c => c.String(nullable: false));
            AddColumn("dbo.Riesgoes", "Zona_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Tarea_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Proceso_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "PeorConsecuencia_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Peligro_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Actividad_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Accidentadoes", "IncidenteID", "dbo.Incidentes");
            DropForeignKey("dbo.Seguimientoes", "AccionViewModel_ID", "dbo.AccionViewModels");
            DropForeignKey("dbo.PlanActions", "AccionViewModel_ID", "dbo.AccionViewModels");
            DropIndex("dbo.Seguimientoes", new[] { "AccionViewModel_ID" });
            DropIndex("dbo.PlanActions", new[] { "AccionViewModel_ID" });
            DropIndex("dbo.Accidentadoes", new[] { "IncidenteID" });
            AlterColumn("dbo.Aplicacions", "Observaciones", c => c.String());
            AlterColumn("dbo.Trabajadors", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Firma", c => c.String());
            AlterColumn("dbo.Trabajadors", "Foto", c => c.String());
            DropColumn("dbo.Trazas", "RiesgoolID");
            DropColumn("dbo.Trazas", "AplicacionID");
            DropColumn("dbo.Controls", "CategoriaEfectividad");
            DropColumn("dbo.Controls", "CategoriaAplicacion");
            DropColumn("dbo.Controls", "RiesgoID");
            DropColumn("dbo.Controls", "AplicacionID");
            DropColumn("dbo.Aplicacions", "TrabajadorID");
            DropColumn("dbo.Aplicacions", "Beneficios");
            DropColumn("dbo.Aplicacions", "Intervencion");
            DropColumn("dbo.Aplicacions", "Finalidad");
            DropColumn("dbo.Aplicacions", "Nombre");
            DropColumn("dbo.Trabajadors", "CargoID");
            DropColumn("dbo.Trabajadors", "DiasPago");
            DropColumn("dbo.Trabajadors", "FechaNomina");
            DropColumn("dbo.Trabajadors", "Nombres");
            DropColumn("dbo.Accions", "Estado");
            DropColumn("dbo.Accions", "Efectiva");
            DropColumn("dbo.Accions", "FechaCierre");
            DropColumn("dbo.Accions", "EficaciaDespues");
            DropColumn("dbo.Accions", "EficaciaAntes");
            DropColumn("dbo.Accions", "Descripcion");
            DropColumn("dbo.Accions", "FuenteAccion");
            DropColumn("dbo.Accions", "TrabajadorID");
            DropColumn("dbo.Accions", "TareaID");
            DropColumn("dbo.Accions", "ActividadID");
            DropColumn("dbo.Accions", "ProcesoID");
            DropColumn("dbo.Accions", "ZonaID");
            DropColumn("dbo.Riesgoes", "AccionID");
            DropColumn("dbo.Riesgoes", "IncidenteID");
            DropColumn("dbo.Riesgoes", "PeorConsecuencia");
            DropColumn("dbo.Riesgoes", "PeligroID");
            DropColumn("dbo.Riesgoes", "TareaID");
            DropColumn("dbo.Riesgoes", "ActividadID");
            DropColumn("dbo.Riesgoes", "ProcesoID");
            DropColumn("dbo.Riesgoes", "ZonaID");
            DropTable("dbo.UserViewModels");
            DropTable("dbo.Users");
            DropTable("dbo.SeguimientoAccions");
            DropTable("dbo.RoleUserVMs");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleOperations");
            DropTable("dbo.RiesgoViewModels");
            DropTable("dbo.PlanAccions");
            DropTable("dbo.Organizations");
            DropTable("dbo.IndicadorViewModels");
            DropTable("dbo.Indicadors");
            DropTable("dbo.IncidenteViewModels");
            DropTable("dbo.Incidentes");
            DropTable("dbo.Documents");
            DropTable("dbo.CreateIndicatorsViewModels");
            DropTable("dbo.AuditRiesgoes");
            DropTable("dbo.AuditaAccions");
            DropTable("dbo.AplicacionVMs");
            DropTable("dbo.Seguimientoes");
            DropTable("dbo.PlanActions");
            DropTable("dbo.AccionViewModels");
            DropTable("dbo.Accidentadoes");
            CreateIndex("dbo.Aplicacions", "Trabajador_ID");
            CreateIndex("dbo.Aplicacions", "Control_ID");
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            CreateIndex("dbo.Accions", "Trabajador_ID");
            CreateIndex("dbo.Accions", "RiesgoID");
            CreateIndex("dbo.Riesgoes", "Zona_ID");
            CreateIndex("dbo.Riesgoes", "Tarea_ID");
            CreateIndex("dbo.Riesgoes", "Proceso_ID");
            CreateIndex("dbo.Riesgoes", "PeorConsecuencia_ID");
            CreateIndex("dbo.Riesgoes", "Peligro_ID");
            CreateIndex("dbo.Riesgoes", "Actividad_ID");
            AddForeignKey("dbo.Riesgoes", "Zona_ID", "dbo.Zonas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Tarea_ID", "dbo.Tareas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Peligro_ID", "dbo.Peligroes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID", cascadeDelete: true);
        }
    }
}
