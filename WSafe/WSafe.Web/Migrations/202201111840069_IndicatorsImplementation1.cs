namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicatorsImplementation1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndicadorViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        IndicadorID = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Definicion = c.String(nullable: false),
                        Numerador = c.String(nullable: false),
                        Denominador = c.String(nullable: false),
                        Formula = c.String(nullable: false),
                        Interpretacion = c.String(nullable: false),
                        Periodicidad = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IndicadorViewModels");
        }
    }
}
