namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RiesgoViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        Rutinaria = c.Boolean(nullable: false),
                        CategoriaPeligroID = c.Int(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        EfectosPosibles = c.Int(nullable: false),
                        NivelesDeficienciaID = c.Int(nullable: false),
                        NivelesDeficiencia = c.Int(nullable: false),
                        NivelesExposicionID = c.Int(nullable: false),
                        NivelesExposicion = c.Int(nullable: false),
                        NivelProbabilidad = c.Int(nullable: false),
                        InterpretacionNP = c.String(),
                        NivelesConsecuenciaID = c.Int(nullable: false),
                        NivelesConsecuencia = c.Int(nullable: false),
                        NivelRiesgo = c.Int(nullable: false),
                        InterpretacionNR = c.String(),
                        AceptabilidadID = c.Int(nullable: false),
                        AceptabilidadNR = c.Int(nullable: false),
                        SignificadoNR = c.String(),
                        NroExpuestos = c.Int(nullable: false),
                        RequisitoLegal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RiesgoViewModels");
        }
    }
}
