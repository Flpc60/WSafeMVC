namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdDBHost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Unsafeacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        Rutinaria = c.Boolean(nullable: false),
                        FechaReporte = c.DateTime(nullable: false),
                        ActCategory = c.Int(nullable: false),
                        Antecedentes = c.String(nullable: false, maxLength: 100),
                        FechaAntecedente = c.DateTime(nullable: false),
                        CategoriaPeligroID = c.Int(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        ActDescription = c.String(nullable: false, maxLength: 100),
                        ProbableConsecuencia = c.String(nullable: false),
                        Recomendations = c.String(nullable: false),
                        WorkerID = c.Int(nullable: false),
                        Worker1ID = c.Int(nullable: false),
                        Worker2ID = c.Int(nullable: false),
                        MovimientID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UnsafeactsListVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Zona = c.String(),
                        Proceso = c.String(),
                        Actividad = c.String(),
                        Tarea = c.String(),
                        ActCategory = c.String(nullable: false),
                        Antecedentes = c.String(maxLength: 100),
                        FechaAntecedente = c.DateTime(nullable: false),
                        CategoriaPeligro = c.String(),
                        Peligro = c.String(),
                        ActDescription = c.String(maxLength: 100),
                        ProbableConsecuencia = c.String(),
                        Recomendations = c.String(),
                        Worker = c.String(),
                        MovimientID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UnsafeactVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaReporte = c.DateTime(nullable: false),
                        ActCategory = c.Int(nullable: false),
                        Antecedentes = c.String(nullable: false, maxLength: 100),
                        FechaAntecedente = c.DateTime(nullable: false),
                        CategoriaPeligroID = c.Int(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        ActDescription = c.String(nullable: false, maxLength: 100),
                        ProbableConsecuencia = c.String(nullable: false),
                        Recomendations = c.String(nullable: false),
                        WorkerID = c.Int(nullable: false),
                        Worker1ID = c.Int(nullable: false),
                        Worker2ID = c.Int(nullable: false),
                        MovimientID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Organizations", "StandardUnsafeacts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardUnsafeacts");
            DropTable("dbo.UnsafeactVMs");
            DropTable("dbo.UnsafeactsListVMs");
            DropTable("dbo.Unsafeacts");
        }
    }
}
