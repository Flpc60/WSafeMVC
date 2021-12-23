namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccionViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        Categoria = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        FuenteAccion = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                        CausasAccion = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Plan = c.String(nullable: false),
                        Seguimiento = c.String(),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        Efectividad = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccionViewModels");
        }
    }
}
