namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanActivity11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivities", "NormaID", c => c.Int(nullable: false));
            DropColumn("dbo.PlanActivities", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "Name", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.PlanActivities", "NormaID");
        }
    }
}
