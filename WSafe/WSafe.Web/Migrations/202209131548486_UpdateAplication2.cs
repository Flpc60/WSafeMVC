namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAplication2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AplicacionVMs", "NivelDeficiencia", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "NivelExposicion", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "NivelConsecuencia", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "Aceptabilidad", c => c.Int(nullable: false));
            DropColumn("dbo.Aplicacions", "NivelProbabilidad");
            DropColumn("dbo.Aplicacions", "NivelRiesgo");
            DropColumn("dbo.Aplicacions", "CategoriaRiesgo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Aplicacions", "CategoriaRiesgo", c => c.String(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelProbabilidad", c => c.Int(nullable: false));
            DropColumn("dbo.AplicacionVMs", "Aceptabilidad");
            DropColumn("dbo.AplicacionVMs", "NivelConsecuencia");
            DropColumn("dbo.AplicacionVMs", "NivelExposicion");
            DropColumn("dbo.AplicacionVMs", "NivelDeficiencia");
        }
    }
}
