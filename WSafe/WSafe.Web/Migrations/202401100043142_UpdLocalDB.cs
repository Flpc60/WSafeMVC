namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdLocalDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "Entregables", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.PlanActivities", "Entregables", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlanActivities", "Entregables", c => c.String(maxLength: 200));
            AlterColumn("dbo.CreatePlanActivityVMs", "Entregables", c => c.String(maxLength: 200));
        }
    }
}
