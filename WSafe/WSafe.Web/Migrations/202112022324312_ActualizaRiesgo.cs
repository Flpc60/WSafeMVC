namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaRiesgo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "CategoriaPeligro_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false, maxLength: 1));
            CreateIndex("dbo.Riesgoes", "CategoriaPeligro_ID");
            AddForeignKey("dbo.Riesgoes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes", "ID", cascadeDelete: true);
            DropColumn("dbo.Riesgoes", "CategoriaPeligroID");
            DropColumn("dbo.Riesgoes", "NivelProbabilidad");
            DropColumn("dbo.Riesgoes", "NivelRiesgo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riesgoes", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "NivelProbabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "CategoriaPeligroID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Riesgoes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes");
            DropIndex("dbo.Riesgoes", new[] { "CategoriaPeligro_ID" });
            AlterColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false));
            DropColumn("dbo.Riesgoes", "CategoriaPeligro_ID");
        }
    }
}
