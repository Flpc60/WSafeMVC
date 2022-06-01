namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanAction2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActions", "AccionViewModel_ID", c => c.Int());
            CreateIndex("dbo.PlanActions", "AccionViewModel_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlanActions", new[] { "AccionViewModel_ID" });
            DropColumn("dbo.PlanActions", "AccionViewModel_ID");
        }
    }
}
