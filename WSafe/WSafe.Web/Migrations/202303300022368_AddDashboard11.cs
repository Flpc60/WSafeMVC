namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDashboard11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        AccidentsProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InductionProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InspectionProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinimalStandardsProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActivitiesPlanProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CapacitationPlanProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResearchAccidentsProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResearchELProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Incdents = c.Int(nullable: false),
                        Accidents = c.Int(nullable: false),
                        Ausentisms = c.Int(nullable: false),
                        Mortality = c.Int(nullable: false),
                        AccidentsFrecuency = c.Int(nullable: false),
                        AccidentsSeverity = c.Int(nullable: false),
                        ELFrecuence = c.Int(nullable: false),
                        ELPrevalence = c.Int(nullable: false),
                        ELIncidence = c.Int(nullable: false),
                        ILIAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Trabajadors", "FechaIngreso", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaRetiro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "OrganizationID", c => c.Int(nullable: false));
            DropColumn("dbo.Trabajadors", "FechaNomina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "FechaNomina", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trabajadors", "OrganizationID");
            DropColumn("dbo.Trabajadors", "FechaRetiro");
            DropColumn("dbo.Trabajadors", "FechaIngreso");
            DropTable("dbo.Dashboards");
        }
    }
}
