namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSiguePlanAnual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivityVMs", "AuditID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.SiguePlanAnuals", "Programed", c => c.Short(nullable: false));
            AlterColumn("dbo.PlanActivities", "Activity", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.PlanActivities", "Entregables", c => c.String(maxLength: 200));
            DropColumn("dbo.PlanActivities", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "Year", c => c.Short(nullable: false));
            AlterColumn("dbo.PlanActivities", "Entregables", c => c.String(maxLength: 100));
            AlterColumn("dbo.PlanActivities", "Activity", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.SiguePlanAnuals", "Programed");
            DropColumn("dbo.PlanActivityVMs", "UserID");
            DropColumn("dbo.PlanActivityVMs", "ClientID");
            DropColumn("dbo.PlanActivityVMs", "OrganizationID");
            DropColumn("dbo.PlanActivityVMs", "AuditID");
        }
    }
}
