namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnnualPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnualPlanVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cycle = c.String(maxLength: 2),
                        Activity = c.String(nullable: false, maxLength: 100),
                        Entregables = c.String(maxLength: 100),
                        Recursos = c.String(),
                        Responsable = c.String(),
                        Observation = c.String(maxLength: 100),
                        StateActivity = c.String(),
                        Programed = c.Short(nullable: false),
                        Executed = c.Short(nullable: false),
                        PorcentajeCumplimiento = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnnualPlanVMs");
        }
    }
}
