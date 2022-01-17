namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccionVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccionViewModels", "CausaAccion", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "SubCausa", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "UltraCausa", c => c.Int(nullable: false));
            DropColumn("dbo.AccionViewModels", "CausasAccion");
            DropColumn("dbo.AccionViewModels", "FechaInicial");
            DropColumn("dbo.AccionViewModels", "FechaFinal");
            DropColumn("dbo.AccionViewModels", "Plan");
            DropColumn("dbo.AccionViewModels", "Seguimiento");
            DropColumn("dbo.AccionViewModels", "FechaSeguimiento");
            DropColumn("dbo.AccionViewModels", "FechaCierre");
            DropColumn("dbo.AccionViewModels", "Efectividad");
            DropColumn("dbo.AccionViewModels", "Prioritaria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccionViewModels", "Prioritaria", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccionViewModels", "Efectividad", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccionViewModels", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccionViewModels", "FechaSeguimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccionViewModels", "Seguimiento", c => c.String());
            AddColumn("dbo.AccionViewModels", "Plan", c => c.String(nullable: false));
            AddColumn("dbo.AccionViewModels", "FechaFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccionViewModels", "FechaInicial", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccionViewModels", "CausasAccion", c => c.Int(nullable: false));
            DropColumn("dbo.AccionViewModels", "UltraCausa");
            DropColumn("dbo.AccionViewModels", "SubCausa");
            DropColumn("dbo.AccionViewModels", "CausaAccion");
        }
    }
}
