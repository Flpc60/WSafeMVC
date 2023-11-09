namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivities", "AuditID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanActivities", "AuditID");
        }
    }
}
