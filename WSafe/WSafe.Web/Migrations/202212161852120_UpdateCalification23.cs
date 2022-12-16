namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalification23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvaluationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        FechaEvaluation = c.String(nullable: false),
                        Cumple = c.Int(nullable: false),
                        NoCumple = c.Int(nullable: false),
                        NoAplica = c.Int(nullable: false),
                        StandarsResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AplicationsResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Activitys = c.Int(nullable: false),
                        Ejecutadas = c.Int(nullable: false),
                        Avance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EvaluationVMs");
        }
    }
}
