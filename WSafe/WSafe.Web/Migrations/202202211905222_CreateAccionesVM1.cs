namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccionesVM1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes");
            DropIndex("dbo.Accions", new[] { "RiesgoID" });
            AddColumn("dbo.Accions", "ZonaID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "EficaciaAntes", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "EficaciaDespues", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "Efectiva", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accions", "Estado", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccionViewModels", "ZonaID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "EficaciaAntes", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "EficaciaDespues", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccionViewModels", "Efectiva", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccionViewModels", "Estado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accions", "RiesgoID");
            DropColumn("dbo.Accions", "CausaAccion");
            DropColumn("dbo.Accions", "SubCausa");
            DropColumn("dbo.Accions", "UltraCausa");
            DropColumn("dbo.AccionViewModels", "RiesgoID");
            DropColumn("dbo.AccionViewModels", "CausaAccion");
            DropColumn("dbo.AccionViewModels", "SubCausa");
            DropColumn("dbo.AccionViewModels", "UltraCausa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccionViewModels", "UltraCausa", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "SubCausa", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "CausaAccion", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "UltraCausa", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "SubCausa", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "CausaAccion", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "RiesgoID", c => c.Int(nullable: false));
            DropColumn("dbo.AccionViewModels", "Estado");
            DropColumn("dbo.AccionViewModels", "Efectiva");
            DropColumn("dbo.AccionViewModels", "FechaCierre");
            DropColumn("dbo.AccionViewModels", "EficaciaDespues");
            DropColumn("dbo.AccionViewModels", "EficaciaAntes");
            DropColumn("dbo.AccionViewModels", "TareaID");
            DropColumn("dbo.AccionViewModels", "ActividadID");
            DropColumn("dbo.AccionViewModels", "ProcesoID");
            DropColumn("dbo.AccionViewModels", "ZonaID");
            DropColumn("dbo.Accions", "Estado");
            DropColumn("dbo.Accions", "Efectiva");
            DropColumn("dbo.Accions", "FechaCierre");
            DropColumn("dbo.Accions", "EficaciaDespues");
            DropColumn("dbo.Accions", "EficaciaAntes");
            DropColumn("dbo.Accions", "TareaID");
            DropColumn("dbo.Accions", "ActividadID");
            DropColumn("dbo.Accions", "ProcesoID");
            DropColumn("dbo.Accions", "ZonaID");
            CreateIndex("dbo.Accions", "RiesgoID");
            AddForeignKey("dbo.Accions", "RiesgoID", "dbo.Riesgoes", "ID", cascadeDelete: true);
        }
    }
}
