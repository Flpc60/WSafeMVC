namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "FuenteAccion", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "Descripcion", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "CausasAccion", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "Plan", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Seguimiento", c => c.String());
            AddColumn("dbo.Accions", "FechaSeguimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "FechaCierre", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "Efectividad", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accions", "Intervenciones");
            DropColumn("dbo.Accions", "Observaciones");
            DropColumn("dbo.Accions", "MedidasControl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accions", "MedidasControl", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "Observaciones", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Intervenciones", c => c.String(nullable: false));
            DropColumn("dbo.Accions", "Efectividad");
            DropColumn("dbo.Accions", "FechaCierre");
            DropColumn("dbo.Accions", "FechaSeguimiento");
            DropColumn("dbo.Accions", "Seguimiento");
            DropColumn("dbo.Accions", "Plan");
            DropColumn("dbo.Accions", "CausasAccion");
            DropColumn("dbo.Accions", "Descripcion");
            DropColumn("dbo.Accions", "FuenteAccion");
        }
    }
}
