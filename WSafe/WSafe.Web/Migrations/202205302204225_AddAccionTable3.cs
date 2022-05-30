namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccionTable3 : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SeguimientoActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        Resultado = c.String(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlanActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Causa = c.Int(nullable: false),
                        Accion = c.String(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Prioritaria = c.Boolean(nullable: false),
                        Costos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
