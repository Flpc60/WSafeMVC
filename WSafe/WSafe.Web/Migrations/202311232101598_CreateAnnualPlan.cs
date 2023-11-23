namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnnualPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiguePlanAnuals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaFinal = c.DateTime(nullable: false),
                        DateSigue = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        StateActivity = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Executed = c.Short(nullable: false),
                        Observation = c.String(maxLength: 100),
                        ActionCategory = c.Int(nullable: false),
                        FileName = c.String(),
                        PlanActivityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlanActivities", t => t.PlanActivityID, cascadeDelete: true)
                .Index(t => t.PlanActivityID);
            
            AddColumn("dbo.PlanActivities", "Entregables", c => c.String(maxLength: 100));
            AddColumn("dbo.PlanActivities", "StateActivity", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "StateCronogram", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "Programed", c => c.Short(nullable: false));
            AddColumn("dbo.PlanActivities", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlanActivities", "ActivityFrequency", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiguePlanAnuals", "PlanActivityID", "dbo.PlanActivities");
            DropIndex("dbo.SiguePlanAnuals", new[] { "PlanActivityID" });
            DropColumn("dbo.PlanActivities", "ActivityFrequency");
            DropColumn("dbo.PlanActivities", "InitialDate");
            DropColumn("dbo.PlanActivities", "Programed");
            DropColumn("dbo.PlanActivities", "StateCronogram");
            DropColumn("dbo.PlanActivities", "UserID");
            DropColumn("dbo.PlanActivities", "ClientID");
            DropColumn("dbo.PlanActivities", "OrganizationID");
            DropColumn("dbo.PlanActivities", "StateActivity");
            DropColumn("dbo.PlanActivities", "Entregables");
            DropTable("dbo.SiguePlanAnuals");
        }
    }
}
