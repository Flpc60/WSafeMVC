namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aplicacions", "NivelDeficiencia", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelExposicion", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelProbabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelConsecuencia", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "CategoriaRiesgo", c => c.String(nullable: false));
            AddColumn("dbo.Aplicacions", "Aceptabilidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Aplicacions", "Aceptabilidad");
            DropColumn("dbo.Aplicacions", "CategoriaRiesgo");
            DropColumn("dbo.Aplicacions", "NivelRiesgo");
            DropColumn("dbo.Aplicacions", "NivelConsecuencia");
            DropColumn("dbo.Aplicacions", "NivelProbabilidad");
            DropColumn("dbo.Aplicacions", "NivelExposicion");
            DropColumn("dbo.Aplicacions", "NivelDeficiencia");
        }
    }
}
