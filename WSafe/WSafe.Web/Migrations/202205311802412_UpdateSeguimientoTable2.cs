namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeguimientoTable2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlanAccions", "Responsable", c => c.String(nullable: false));
            AlterColumn("dbo.SeguimientoAccions", "Responsable", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeguimientoAccions", "Responsable", c => c.String());
            AlterColumn("dbo.PlanAccions", "Responsable", c => c.String());
        }
    }
}
