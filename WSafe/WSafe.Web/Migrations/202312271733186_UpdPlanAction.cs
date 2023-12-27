namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdPlanAction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActions", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanActions", "UserID");
            DropColumn("dbo.PlanActions", "ClientID");
            DropColumn("dbo.PlanActions", "OrganizationID");
        }
    }
}
