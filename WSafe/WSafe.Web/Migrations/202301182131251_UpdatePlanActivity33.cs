namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanActivity33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivities", "Fundamentos", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanActivities", "Fundamentos");
        }
    }
}
