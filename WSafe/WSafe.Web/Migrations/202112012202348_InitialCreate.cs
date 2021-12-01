namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Peligroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoriaPeligroID = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Procesoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Riesgoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rutinaria = c.Boolean(nullable: false),
                        CategoriaPeligroID = c.Int(nullable: false),
                        EfectosPosibles = c.Int(nullable: false),
                        NivelDeficiencia = c.Int(nullable: false),
                        NivelExposicion = c.Int(nullable: false),
                        NivelProbabilidad = c.Int(nullable: false),
                        NivelConsecuencia = c.Int(nullable: false),
                        NivelRiesgo = c.Int(nullable: false),
                        CategoriaRiesgo = c.String(nullable: false),
                        Aceptabilidad = c.Int(nullable: false),
                        NroExpuestos = c.Int(nullable: false),
                        RequisitoLegal = c.Boolean(nullable: false),
                        Actividad_ID = c.Int(nullable: false),
                        Peligro_ID = c.Int(nullable: false),
                        PeorConsecuencia_ID = c.Int(nullable: false),
                        Proceso_ID = c.Int(nullable: false),
                        Tarea_ID = c.Int(nullable: false),
                        Zona_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Actividads", t => t.Actividad_ID, cascadeDelete: true)
                .ForeignKey("dbo.Peligroes", t => t.Peligro_ID, cascadeDelete: true)
                .ForeignKey("dbo.Consecuencias", t => t.PeorConsecuencia_ID, cascadeDelete: true)
                .ForeignKey("dbo.Procesoes", t => t.Proceso_ID, cascadeDelete: true)
                .ForeignKey("dbo.Tareas", t => t.Tarea_ID, cascadeDelete: true)
                .ForeignKey("dbo.Zonas", t => t.Zona_ID, cascadeDelete: true)
                .Index(t => t.Actividad_ID)
                .Index(t => t.Peligro_ID)
                .Index(t => t.PeorConsecuencia_ID)
                .Index(t => t.Proceso_ID)
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
                        Intervenciones = c.String(nullable: false),
                        Observaciones = c.String(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        MedidasControl = c.Int(nullable: false),
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
                        PrimerApellido = c.String(nullable: false),
                        SegundoApellido = c.String(),
                        PrimerNombre = c.String(nullable: false),
                        SegundoNombre = c.String(),
                        Documento = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Foto = c.String(),
                        Firma = c.String(),
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
                "dbo.Aplicacions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        CategoriaAplicacion = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaEfectividad = c.Int(nullable: false),
                        Observaciones = c.String(),
                        Control_ID = c.Int(nullable: false),
                        Trabajador_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Controls", t => t.Control_ID, cascadeDelete: true)
                .ForeignKey("dbo.Trabajadors", t => t.Trabajador_ID, cascadeDelete: true)
                .ForeignKey("dbo.Riesgoes", t => t.RiesgoID, cascadeDelete: true)
                .Index(t => t.RiesgoID)
                .Index(t => t.Control_ID)
                .Index(t => t.Trabajador_ID);
            
            CreateTable(
                "dbo.Controls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        Nombre = c.String(nullable: false),
                        Finalidad = c.Int(nullable: false),
                        Intervencion = c.Int(nullable: false),
                        Beneficios = c.String(),
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
                        EstadoActual = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Controls", t => t.ControlID, cascadeDelete: true)
                .Index(t => t.ControlID);
            
            CreateTable(
                "dbo.Consecuencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NivelConsecuencia = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tareas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
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
            DropForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes");
            DropForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias");
            DropForeignKey("dbo.Riesgoes", "Peligro_ID", "dbo.Peligroes");
            DropForeignKey("dbo.Aplicacions", "RiesgoID", "dbo.Riesgoes");
            DropForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls");
            DropForeignKey("dbo.Trazas", "ControlID", "dbo.Controls");
            DropForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads");
            DropForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes");
            DropForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors");
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Trazas", new[] { "ControlID" });
            DropIndex("dbo.Aplicacions", new[] { "Trabajador_ID" });
            DropIndex("dbo.Aplicacions", new[] { "Control_ID" });
            DropIndex("dbo.Aplicacions", new[] { "RiesgoID" });
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropIndex("dbo.Accions", new[] { "Trabajador_ID" });
            DropIndex("dbo.Accions", new[] { "RiesgoID" });
            DropIndex("dbo.Riesgoes", new[] { "Zona_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Tarea_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Proceso_ID" });
            DropIndex("dbo.Riesgoes", new[] { "PeorConsecuencia_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Peligro_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Actividad_ID" });
            DropTable("dbo.Zonas");
            DropTable("dbo.Tareas");
            DropTable("dbo.Consecuencias");
            DropTable("dbo.Trazas");
            DropTable("dbo.Controls");
            DropTable("dbo.Aplicacions");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Trabajadors");
            DropTable("dbo.Accions");
            DropTable("dbo.Riesgoes");
            DropTable("dbo.Procesoes");
            DropTable("dbo.Peligroes");
            DropTable("dbo.CategoriaPeligroes");
            DropTable("dbo.Actividads");
        }
    }
}
