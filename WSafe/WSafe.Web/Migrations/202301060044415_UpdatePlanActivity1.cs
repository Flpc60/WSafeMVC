namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanActivity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivities", "Recurso", c => c.Int(nullable: false));
            DropColumn("dbo.PlanActivities", "Presupuesto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "Presupuesto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PlanActivities", "Recurso");
        }
    }
}
