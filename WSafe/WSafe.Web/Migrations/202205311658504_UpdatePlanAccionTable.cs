namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlanAccionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanAccions",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    AccionID = c.Int(nullable: false),
                    FechaInicial = c.DateTime(nullable: false),
                    FechaFinal = c.DateTime(nullable: false),
                    Causa = c.Int(nullable: false),
                    Accion = c.String(nullable: false),
                    TrabajadorID = c.Int(nullable: false),
                    Prioritaria = c.Int(nullable: false),
                    Costos = c.Decimal(nullable: true),
                    Responsable = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accions", t => t.AccionID, cascadeDelete: true)
                .Index(t => t.AccionID);
        }

        public override void Down()
        {
        }
    }
}
