namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgoVM2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IntervencionVMs", "MatrizRiesgosVM_ID", "dbo.MatrizRiesgosVMs");
            DropIndex("dbo.IntervencionVMs", new[] { "MatrizRiesgosVM_ID" });
            AddColumn("dbo.MatrizRiesgosVMs", "Eliminacion", c => c.String());
            AddColumn("dbo.MatrizRiesgosVMs", "Sustitucion", c => c.String());
            AddColumn("dbo.MatrizRiesgosVMs", "Ingenieria", c => c.String());
            AddColumn("dbo.MatrizRiesgosVMs", "Administracion", c => c.String());
            AddColumn("dbo.MatrizRiesgosVMs", "Señalizacion", c => c.String());
            AddColumn("dbo.MatrizRiesgosVMs", "EPP", c => c.String());
            DropTable("dbo.IntervencionVMs");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.MatrizRiesgosVMs", "EPP");
            DropColumn("dbo.MatrizRiesgosVMs", "Señalizacion");
            DropColumn("dbo.MatrizRiesgosVMs", "Administracion");
            DropColumn("dbo.MatrizRiesgosVMs", "Ingenieria");
            DropColumn("dbo.MatrizRiesgosVMs", "Sustitucion");
            DropColumn("dbo.MatrizRiesgosVMs", "Eliminacion");
            CreateIndex("dbo.IntervencionVMs", "MatrizRiesgosVM_ID");
            AddForeignKey("dbo.IntervencionVMs", "MatrizRiesgosVM_ID", "dbo.MatrizRiesgosVMs", "ID");
        }
    }
}
