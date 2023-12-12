namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdAnnualPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnnualPlanVMs", "Seguimients", c => c.String(maxLength: 200));
            AlterColumn("dbo.AnnualPlanVMs", "Observation", c => c.String(maxLength: 200));
            AlterColumn("dbo.SiguePlanAnuals", "Observation", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SiguePlanAnuals", "Observation", c => c.String(maxLength: 100));
            AlterColumn("dbo.AnnualPlanVMs", "Observation", c => c.String(maxLength: 100));
            DropColumn("dbo.AnnualPlanVMs", "Seguimients");
        }
    }
}
