namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiesgoViewModels", "NivelDeficiencia", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelExposicion", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelConsecuencia", c => c.Int(nullable: false));
            DropColumn("dbo.Riesgoes", "CategoriaRiesgo");
            DropColumn("dbo.RiesgoViewModels", "NivelesDeficienciaID");
            DropColumn("dbo.RiesgoViewModels", "NivelesExposicionID");
            DropColumn("dbo.RiesgoViewModels", "NivelProbabilidad");
            DropColumn("dbo.RiesgoViewModels", "InterpretacionNP");
            DropColumn("dbo.RiesgoViewModels", "NivelesConsecuenciaID");
            DropColumn("dbo.RiesgoViewModels", "NivelRiesgo");
            DropColumn("dbo.RiesgoViewModels", "CategoriaRiesgo");
            DropColumn("dbo.RiesgoViewModels", "InterpretacionNR");
            DropColumn("dbo.RiesgoViewModels", "AceptabilidadID");
            DropColumn("dbo.RiesgoViewModels", "SignificadoNR");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RiesgoViewModels", "SignificadoNR", c => c.String());
            AddColumn("dbo.RiesgoViewModels", "AceptabilidadID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "InterpretacionNR", c => c.String());
            AddColumn("dbo.RiesgoViewModels", "CategoriaRiesgo", c => c.String(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelesConsecuenciaID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "InterpretacionNP", c => c.String());
            AddColumn("dbo.RiesgoViewModels", "NivelProbabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelesExposicionID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelesDeficienciaID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false, maxLength: 1));
            DropColumn("dbo.RiesgoViewModels", "NivelConsecuencia");
            DropColumn("dbo.RiesgoViewModels", "NivelExposicion");
            DropColumn("dbo.RiesgoViewModels", "NivelDeficiencia");
        }
    }
}
