namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditRiesgoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        Rutinaria = c.Boolean(nullable: false),
                        PeligroID = c.Int(nullable: false),
                        EfectosPosibles = c.Int(nullable: false),
                        NivelDeficiencia = c.Int(nullable: false),
                        NivelExposicion = c.Int(nullable: false),
                        NivelProbabilidad = c.Int(nullable: false),
                        NivelConsecuencia = c.Int(nullable: false),
                        NivelRiesgo = c.Int(nullable: false),
                        Aceptabilidad = c.Int(nullable: false),
                        NroExpuestos = c.Int(nullable: false),
                        RequisitoLegal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditRiesgoes");
        }
    }
}
