namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlan55 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MinimalsStandardsVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        NIT = c.String(),
                        RazonSocial = c.String(),
                        Direccion = c.String(),
                        Municip = c.String(),
                        Department = c.String(),
                        ClaseRiesgo = c.String(),
                        EconomicActivity = c.String(),
                        NumeroTrabajadores = c.Int(nullable: false),
                        ResponsableSGSST = c.String(),
                        DocumentResponsable = c.String(),
                        FechaEvaluation = c.DateTime(nullable: false),
                        Cumple = c.Int(nullable: false),
                        NoCumple = c.Int(nullable: false),
                        NoAplica = c.Int(nullable: false),
                        StandarsResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AplicationsResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CalificationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EvaluationID = c.Int(nullable: false),
                        NormaID = c.Int(nullable: false),
                        Ciclo = c.String(),
                        Item = c.String(),
                        Name = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Standard = c.String(),
                        Cumple = c.Boolean(nullable: false),
                        TxtCumple = c.String(),
                        NoCumple = c.Boolean(nullable: false),
                        TxtNoCumple = c.String(),
                        Justify = c.Boolean(nullable: false),
                        TxtJustify = c.String(),
                        NoJustify = c.Boolean(nullable: false),
                        TxtNoJustify = c.String(),
                        Valoration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observation = c.String(),
                        Verification = c.String(),
                        MinimalsStandardsVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MinimalsStandardsVMs", t => t.MinimalsStandardsVM_ID)
                .Index(t => t.MinimalsStandardsVM_ID);
            
            CreateTable(
                "dbo.PlanActivityVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EvaluationID = c.Int(nullable: false),
                        NormaID = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                        FechaFinal = c.DateTime(nullable: false),
                        FechaCumplimiento = c.String(),
                        Activity = c.String(),
                        Recurso = c.Int(nullable: false),
                        TxtRecurso = c.String(),
                        ActionCategory = c.Int(nullable: false),
                        TxtActionCategory = c.String(),
                        Observation = c.String(),
                        Ciclo = c.String(),
                        Item = c.String(),
                        Name = c.String(),
                        Fundamentos = c.String(),
                        MinimalsStandardsVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MinimalsStandardsVMs", t => t.MinimalsStandardsVM_ID)
                .Index(t => t.MinimalsStandardsVM_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanActivityVMs", "MinimalsStandardsVM_ID", "dbo.MinimalsStandardsVMs");
            DropForeignKey("dbo.CalificationVMs", "MinimalsStandardsVM_ID", "dbo.MinimalsStandardsVMs");
            DropIndex("dbo.PlanActivityVMs", new[] { "MinimalsStandardsVM_ID" });
            DropIndex("dbo.CalificationVMs", new[] { "MinimalsStandardsVM_ID" });
            DropTable("dbo.PlanActivityVMs");
            DropTable("dbo.CalificationVMs");
            DropTable("dbo.MinimalsStandardsVMs");
        }
    }
}
