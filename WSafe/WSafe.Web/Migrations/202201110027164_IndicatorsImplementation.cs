namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicatorsImplementation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Indicadors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
            DropTable("dbo.Indicadors");
        }
    }
}
