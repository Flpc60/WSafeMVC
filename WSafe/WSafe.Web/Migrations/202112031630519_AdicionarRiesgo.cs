namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarRiesgo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false, maxLength: 1));
            DropColumn("dbo.Riesgoes", "CategoriaPeligroID");
            DropColumn("dbo.Riesgoes", "NivelProbabilidad");
            DropColumn("dbo.Riesgoes", "NivelRiesgo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riesgoes", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "NivelProbabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "CategoriaPeligroID", c => c.Int(nullable: false));
            AlterColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false));
        }
    }
}
