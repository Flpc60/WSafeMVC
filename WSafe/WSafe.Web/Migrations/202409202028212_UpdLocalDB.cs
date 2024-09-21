namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdLocalDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlanCategoriy = c.Int(nullable: false),
                        Objetive = c.String(nullable: false, maxLength: 500),
                        TrabajadorID = c.Int(nullable: false),
                        Resources = c.String(nullable: false, maxLength: 500),
                        Before = c.String(nullable: false, maxLength: 500),
                        During = c.String(nullable: false, maxLength: 500),
                        After = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Traceabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionPlanID = c.Int(nullable: false),
                        DateSigue = c.DateTime(nullable: false),
                        Efectividad = c.Boolean(nullable: false),
                        Observations = c.String(nullable: false, maxLength: 200),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        GenerateAction = c.Boolean(nullable: false),
                        Finality = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        AplicationCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActionPlans", t => t.ActionPlanID, cascadeDelete: true)
                .Index(t => t.ActionPlanID);
            
            CreateTable(
                "dbo.Amenazas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 250),
                        CategoryAmenaza = c.Int(nullable: false),
                        OrigenAmenaza = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CalificationAmenazas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryAmenaza = c.Int(nullable: false),
                        AmenzaID = c.Int(nullable: false),
                        Calification = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Amenaza_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Amenazas", t => t.Amenaza_ID, cascadeDelete: true)
                .Index(t => t.Amenaza_ID);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VulnerabilityID = c.Int(nullable: false),
                        CategoriaAplicacion = c.Int(nullable: false),
                        Finalidad = c.Int(nullable: false),
                        Intervencion = c.Int(nullable: false),
                        Beneficios = c.String(nullable: false, maxLength: 200),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrabajadorID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Observaciones = c.String(nullable: false),
                        NivelDeficiencia = c.Short(nullable: false),
                        NivelExposicion = c.Short(nullable: false),
                        NivelConsecuencia = c.Short(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vulnerabilities", t => t.VulnerabilityID, cascadeDelete: true)
                .Index(t => t.VulnerabilityID);
            
            CreateTable(
                "dbo.Pons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionPlanID = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Resources = c.String(nullable: false, maxLength: 500),
                        Document = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vulnerabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryAmenaza = c.Int(nullable: false),
                        AmenazaID = c.Int(nullable: false),
                        EvaluationConceptID = c.Int(nullable: false),
                        Response = c.Int(nullable: false),
                        Observation = c.String(maxLength: 500),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Amenazas", t => t.AmenazaID, cascadeDelete: true)
                .ForeignKey("dbo.EvaluationConcepts", t => t.EvaluationConceptID, cascadeDelete: true)
                .Index(t => t.AmenazaID)
                .Index(t => t.EvaluationConceptID);
            
            CreateTable(
                "dbo.EvaluationConcepts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VulnerabilitiType = c.Int(nullable: false),
                        EvaluationPerson = c.Int(nullable: false),
                        EvaluationRecurso = c.Int(nullable: false),
                        EvaluationSystem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "VulnerabilityID", "dbo.Vulnerabilities");
            DropForeignKey("dbo.Vulnerabilities", "EvaluationConceptID", "dbo.EvaluationConcepts");
            DropForeignKey("dbo.Vulnerabilities", "AmenazaID", "dbo.Amenazas");
            DropForeignKey("dbo.CalificationAmenazas", "Amenaza_ID", "dbo.Amenazas");
            DropForeignKey("dbo.Traceabilities", "ActionPlanID", "dbo.ActionPlans");
            DropIndex("dbo.Vulnerabilities", new[] { "EvaluationConceptID" });
            DropIndex("dbo.Vulnerabilities", new[] { "AmenazaID" });
            DropIndex("dbo.Interventions", new[] { "VulnerabilityID" });
            DropIndex("dbo.CalificationAmenazas", new[] { "Amenaza_ID" });
            DropIndex("dbo.Traceabilities", new[] { "ActionPlanID" });
            DropTable("dbo.EvaluationConcepts");
            DropTable("dbo.Vulnerabilities");
            DropTable("dbo.Pons");
            DropTable("dbo.Interventions");
            DropTable("dbo.CalificationAmenazas");
            DropTable("dbo.Amenazas");
            DropTable("dbo.Traceabilities");
            DropTable("dbo.ActionPlans");
        }
    }
}
