namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanAcc3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlanAccions", "Accion", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PlanAccions", "Responsable", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlanAccions", "Responsable", c => c.String());
            AlterColumn("dbo.PlanAccions", "Accion", c => c.String(nullable: false));
        }
    }
}
