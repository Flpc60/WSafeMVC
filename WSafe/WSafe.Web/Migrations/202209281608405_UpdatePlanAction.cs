namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanAction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActions", "ActionCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanActions", "ActionCategory");
        }
    }
}
