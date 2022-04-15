namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgosVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatrizRiesgosVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Proceso = c.String(),
                        Zona = c.String(),
                        Actividad = c.String(),
                        Rutinaria = c.Boolean(nullable: false),
                        CategoriaPeligro = c.String(),
                        Peligro = c.String(),
                        EfectosPosibles = c.String(),
                        Fuente = c.String(),
                        Medio = c.String(),
                        Individuo = c.String(),
                        NivelDeficiencia = c.Int(nullable: false),
                        NivelExposicion = c.Int(nullable: false),
                        NivelProbabilidad = c.Int(nullable: false),
                        InterpretaNP = c.String(),
                        NivelConsecuencia = c.Int(nullable: false),
                        NivelRiesgo = c.Int(nullable: false),
                        CategoriaRiesgo = c.String(),
                        AceptabilidadNR = c.String(),
                        SignificadoNR = c.String(),
                        NroExpuestos = c.Int(nullable: false),
                        PeorConsecuencia = c.String(),
                        RequisitoLegal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IntervencionVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Eliminacion = c.String(),
                        Sustitucion = c.String(),
                        Ingenieria = c.String(),
                        Administracion = c.String(),
                        Señalizacion = c.String(),
                        EPP = c.String(),
                        MatrizRiesgosVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MatrizRiesgosVMs", t => t.MatrizRiesgosVM_ID)
                .Index(t => t.MatrizRiesgosVM_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntervencionVMs", "MatrizRiesgosVM_ID", "dbo.MatrizRiesgosVMs");
            DropIndex("dbo.IntervencionVMs", new[] { "MatrizRiesgosVM_ID" });
            DropTable("dbo.IntervencionVMs");
            DropTable("dbo.MatrizRiesgosVMs");
        }
    }
}
