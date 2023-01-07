namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanActivity22 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlanActivities", "Ciclo");
            DropColumn("dbo.PlanActivities", "Item");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "Item", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.PlanActivities", "Ciclo", c => c.String(nullable: false, maxLength: 2));
        }
    }
}
