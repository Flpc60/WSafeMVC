namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanActivities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EvaluationID = c.Int(nullable: false),
                        NormaID = c.Int(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Activity = c.String(nullable: false, maxLength: 100),
                        TrabajadorID = c.Int(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActionCategory = c.Int(nullable: false),
                        Observation = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlanActivities");
        }
    }
}
