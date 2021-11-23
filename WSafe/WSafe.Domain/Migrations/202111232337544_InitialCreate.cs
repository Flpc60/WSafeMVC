namespace WSafe.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Riesgoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rutinaria = c.Boolean(nullable: false),
                        EfectosPosibles = c.Int(nullable: false),
                        NivelDeficiencia = c.Int(nullable: false),
                        NivelExposicion = c.Int(nullable: false),
                        NivelProbabilidad = c.Int(nullable: false),
                        NivelConsecuencias = c.Int(nullable: false),
                        NivelRiesgo = c.Int(nullable: false),
                        NroExpuestos = c.Int(nullable: false),
                        RequisitoLegal = c.Boolean(nullable: false),
                        Actividad_ID = c.Int(nullable: false),
                        CategoriaPeligro_ID = c.Int(nullable: false),
                        ControlesFuente_ID = c.Int(),
                        ControlesIndividuo_ID = c.Int(),
                        ControlesMedio_ID = c.Int(),
                        Eliminacion_ID = c.Int(),
                        Peligro_ID = c.Int(nullable: false),
                        PeorConsecuencia_ID = c.Int(nullable: false),
                        Proceso_ID = c.Int(nullable: false),
                        Sustitucion_ID = c.Int(),
                        Tarea_ID = c.Int(nullable: false),
                        Zona_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Actividads", t => t.Actividad_ID, cascadeDelete: true)
                .ForeignKey("dbo.CategoriaPeligroes", t => t.CategoriaPeligro_ID, cascadeDelete: true)
                .ForeignKey("dbo.Aplicacions", t => t.ControlesFuente_ID)
                .ForeignKey("dbo.Aplicacions", t => t.ControlesIndividuo_ID)
                .ForeignKey("dbo.Aplicacions", t => t.ControlesMedio_ID)
                .ForeignKey("dbo.Aplicacions", t => t.Eliminacion_ID)
                .ForeignKey("dbo.Peligroes", t => t.Peligro_ID, cascadeDelete: true)
                .ForeignKey("dbo.Consecuencias", t => t.PeorConsecuencia_ID, cascadeDelete: true)
                .ForeignKey("dbo.Procesoes", t => t.Proceso_ID, cascadeDelete: true)
                .ForeignKey("dbo.Aplicacions", t => t.Sustitucion_ID)
                .ForeignKey("dbo.Tareas", t => t.Tarea_ID, cascadeDelete: true)
                .ForeignKey("dbo.Zonas", t => t.Zona_ID, cascadeDelete: true)
                .Index(t => t.Actividad_ID)
                .Index(t => t.CategoriaPeligro_ID)
                .Index(t => t.ControlesFuente_ID)
                .Index(t => t.ControlesIndividuo_ID)
                .Index(t => t.ControlesMedio_ID)
                .Index(t => t.Eliminacion_ID)
                .Index(t => t.Peligro_ID)
                .Index(t => t.PeorConsecuencia_ID)
                .Index(t => t.Proceso_ID)
                .Index(t => t.Sustitucion_ID)
                .Index(t => t.Tarea_ID)
                .Index(t => t.Zona_ID);
            
            CreateTable(
                "dbo.Accions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        Categoria = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Observaciones = c.String(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Intervenciones = c.String(nullable: false),
                        Trabajador_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trabajadors", t => t.Trabajador_ID, cascadeDelete: true)
                .ForeignKey("dbo.Riesgoes", t => t.RiesgoID, cascadeDelete: true)
                .Index(t => t.RiesgoID)
                .Index(t => t.Trabajador_ID);
            
            CreateTable(
                "dbo.Trabajadors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrimerApellido = c.String(nullable: false, maxLength: 50),
                        SegundoApellido = c.String(),
                        PrimerNombre = c.String(nullable: false, maxLength: 50),
                        SegundoNombre = c.String(),
                        Documento = c.String(maxLength: 20),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Foto = c.String(),
                        Firma = c.String(nullable: false),
                        Email = c.String(),
                        Genero = c.Int(nullable: false),
                        EstadoCivil = c.Int(nullable: false),
                        TipoVinculacion = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        EPS = c.String(nullable: false),
                        AFP = c.String(nullable: false),
                        ARL = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        Telefonos = c.String(nullable: false),
                        AltoRiesgo = c.Boolean(nullable: false),
                        ActividadAltoRiesgo = c.String(),
                        Cargo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cargoes", t => t.Cargo_ID, cascadeDelete: true)
                .Index(t => t.Cargo_ID);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Actividads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CategoriaPeligroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Aplicacions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaEfectividad = c.Int(nullable: false),
                        Observaciones = c.String(),
                        Control_ID = c.Int(),
                        Trabajador_ID = c.Int(),
                        Riesgo_ID = c.Int(),
                        Riesgo_ID1 = c.Int(),
                        Riesgo_ID2 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Controls", t => t.Control_ID)
                .ForeignKey("dbo.Trabajadors", t => t.Trabajador_ID)
                .ForeignKey("dbo.Riesgoes", t => t.Riesgo_ID)
                .ForeignKey("dbo.Riesgoes", t => t.Riesgo_ID1)
                .ForeignKey("dbo.Riesgoes", t => t.Riesgo_ID2)
                .Index(t => t.Control_ID)
                .Index(t => t.Trabajador_ID)
                .Index(t => t.Riesgo_ID)
                .Index(t => t.Riesgo_ID1)
                .Index(t => t.Riesgo_ID2);
            
            CreateTable(
                "dbo.Controls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        Nombre = c.String(nullable: false),
                        Finalidad = c.Int(nullable: false),
                        Categoria = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Efectividad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Trazas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        ControlID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Efectividad = c.Int(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Controls", t => t.ControlID, cascadeDelete: true)
                .Index(t => t.ControlID);
            
            CreateTable(
                "dbo.Peligroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoriaPeligroID = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoriaPeligroes", t => t.CategoriaPeligroID, cascadeDelete: true)
                .Index(t => t.CategoriaPeligroID);
            
            CreateTable(
                "dbo.Consecuencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoriaPeligroID = c.Int(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Peligroes", t => t.PeligroID, cascadeDelete: true)
                .Index(t => t.PeligroID);
            
            CreateTable(
                "dbo.Procesoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tareas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Zonas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Riesgoes", "Zona_ID", "dbo.Zonas");
            DropForeignKey("dbo.Riesgoes", "Tarea_ID", "dbo.Tareas");
            DropForeignKey("dbo.Riesgoes", "Sustitucion_ID", "dbo.Aplicacions");
            DropForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes");
            DropForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias");
            DropForeignKey("dbo.Consecuencias", "PeligroID", "dbo.Peligroes");
            DropForeignKey("dbo.Riesgoes", "Peligro_ID", "dbo.Peligroes");
            DropForeignKey("dbo.Peligroes", "CategoriaPeligroID", "dbo.CategoriaPeligroes");
            DropForeignKey("dbo.Riesgoes", "Eliminacion_ID", "dbo.Aplicacions");
            DropForeignKey("dbo.Riesgoes", "ControlesMedio_ID", "dbo.Aplicacions");
            DropForeignKey("dbo.Aplicacions", "Riesgo_ID2", "dbo.Riesgoes");
            DropForeignKey("dbo.Riesgoes", "ControlesIndividuo_ID", "dbo.Aplicacions");
            DropForeignKey("dbo.Riesgoes", "ControlesFuente_ID", "dbo.Aplicacions");
            DropForeignKey("dbo.Aplicacions", "Riesgo_ID1", "dbo.Riesgoes");
            DropForeignKey("dbo.Aplicacions", "Riesgo_ID", "dbo.Riesgoes");
            DropForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls");
            DropForeignKey("dbo.Trazas", "ControlID", "dbo.Controls");
            DropForeignKey("dbo.Riesgoes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes");
            DropForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads");
            DropForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes");
            DropForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Consecuencias", new[] { "PeligroID" });
            DropIndex("dbo.Peligroes", new[] { "CategoriaPeligroID" });
            DropIndex("dbo.Trazas", new[] { "ControlID" });
            DropIndex("dbo.Aplicacions", new[] { "Riesgo_ID2" });
            DropIndex("dbo.Aplicacions", new[] { "Riesgo_ID1" });
            DropIndex("dbo.Aplicacions", new[] { "Riesgo_ID" });
            DropIndex("dbo.Aplicacions", new[] { "Trabajador_ID" });
            DropIndex("dbo.Aplicacions", new[] { "Control_ID" });
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropIndex("dbo.Accions", new[] { "Trabajador_ID" });
            DropIndex("dbo.Accions", new[] { "RiesgoID" });
            DropIndex("dbo.Riesgoes", new[] { "Zona_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Tarea_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Sustitucion_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Proceso_ID" });
            DropIndex("dbo.Riesgoes", new[] { "PeorConsecuencia_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Peligro_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Eliminacion_ID" });
            DropIndex("dbo.Riesgoes", new[] { "ControlesMedio_ID" });
            DropIndex("dbo.Riesgoes", new[] { "ControlesIndividuo_ID" });
            DropIndex("dbo.Riesgoes", new[] { "ControlesFuente_ID" });
            DropIndex("dbo.Riesgoes", new[] { "CategoriaPeligro_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Actividad_ID" });
            DropTable("dbo.Zonas");
            DropTable("dbo.Tareas");
            DropTable("dbo.Procesoes");
            DropTable("dbo.Consecuencias");
            DropTable("dbo.Peligroes");
            DropTable("dbo.Trazas");
            DropTable("dbo.Controls");
            DropTable("dbo.Aplicacions");
            DropTable("dbo.CategoriaPeligroes");
            DropTable("dbo.Actividads");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Trabajadors");
            DropTable("dbo.Accions");
            DropTable("dbo.Riesgoes");
        }
    }
}
