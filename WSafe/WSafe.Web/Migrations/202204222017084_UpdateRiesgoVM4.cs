namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgoVM4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "AccionID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "AccionID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "CategoriaAplicacion", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "Finalidad", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "Intervencion", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "Beneficios", c => c.String());
            AddColumn("dbo.RiesgoViewModels", "Presupuesto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiesgoViewModels", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "FechaInicial", c => c.DateTime(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "FechaFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "Observaciones", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "Observaciones");
            DropColumn("dbo.RiesgoViewModels", "FechaFinal");
            DropColumn("dbo.RiesgoViewModels", "FechaInicial");
            DropColumn("dbo.RiesgoViewModels", "TrabajadorID");
            DropColumn("dbo.RiesgoViewModels", "Presupuesto");
            DropColumn("dbo.RiesgoViewModels", "Beneficios");
            DropColumn("dbo.RiesgoViewModels", "Intervencion");
            DropColumn("dbo.RiesgoViewModels", "Finalidad");
            DropColumn("dbo.RiesgoViewModels", "CategoriaAplicacion");
            DropColumn("dbo.RiesgoViewModels", "Nombre");
            DropColumn("dbo.Riesgoes", "AccionID");
            DropColumn("dbo.Incidentes", "AccionID");
            DropColumn("dbo.Incidentes", "RiesgoID");
            DropColumn("dbo.Accions", "RiesgoID");
        }
    }
}
