namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnnualPlan1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "InitialDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CreatePlanActivityVMs", "FechaFinal", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "FechaFinal", c => c.String(nullable: false));
            AlterColumn("dbo.CreatePlanActivityVMs", "InitialDate", c => c.String(nullable: false));
        }
    }
}
