namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIntervencion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AplicacionVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiesgoID = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        CategoriaAplicacion = c.Int(nullable: false),
                        Finalidad = c.Int(nullable: false),
                        Intervencion = c.Int(nullable: false),
                        Beneficios = c.String(),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrabajadorID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Observaciones = c.String(nullable: false),
                        RiesgoViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RiesgoViewModels", t => t.RiesgoViewModel_ID)
                .Index(t => t.RiesgoViewModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AplicacionVMs", "RiesgoViewModel_ID", "dbo.RiesgoViewModels");
            DropIndex("dbo.AplicacionVMs", new[] { "RiesgoViewModel_ID" });
            DropTable("dbo.AplicacionVMs");
        }
    }
}
