namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdSigueAnnualPlan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SiguePlanAnuals", "FechaFinal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiguePlanAnuals", "FechaFinal", c => c.DateTime(nullable: false));
        }
    }
}
