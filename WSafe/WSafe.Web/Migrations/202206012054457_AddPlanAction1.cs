namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanAction1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Causa = c.Int(nullable: false),
                        Accion = c.String(nullable: false, maxLength: 100),
                        TrabajadorID = c.Int(nullable: false),
                        Prioritaria = c.Boolean(nullable: false),
                        Costos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Responsable = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlanActions");
        }
    }
}
