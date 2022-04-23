namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableRiesgo2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads");
            DropForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes");
            DropForeignKey("dbo.Riesgoes", "Tarea_ID", "dbo.Tareas");
            DropForeignKey("dbo.Riesgoes", "Zona_ID", "dbo.Zonas");
            DropIndex("dbo.Riesgoes", new[] { "Actividad_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Proceso_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Tarea_ID" });
            DropIndex("dbo.Riesgoes", new[] { "Zona_ID" });
            AddColumn("dbo.Riesgoes", "ZonaID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "TareaID", c => c.Int(nullable: false));
            DropColumn("dbo.Riesgoes", "Actividad_ID");
            DropColumn("dbo.Riesgoes", "Proceso_ID");
            DropColumn("dbo.Riesgoes", "Tarea_ID");
            DropColumn("dbo.Riesgoes", "Zona_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riesgoes", "Zona_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Tarea_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Proceso_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "Actividad_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Riesgoes", "TareaID");
            DropColumn("dbo.Riesgoes", "ActividadID");
            DropColumn("dbo.Riesgoes", "ProcesoID");
            DropColumn("dbo.Riesgoes", "ZonaID");
            CreateIndex("dbo.Riesgoes", "Zona_ID");
            CreateIndex("dbo.Riesgoes", "Tarea_ID");
            CreateIndex("dbo.Riesgoes", "Proceso_ID");
            CreateIndex("dbo.Riesgoes", "Actividad_ID");
            AddForeignKey("dbo.Riesgoes", "Zona_ID", "dbo.Zonas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Tarea_ID", "dbo.Tareas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Proceso_ID", "dbo.Procesoes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Riesgoes", "Actividad_ID", "dbo.Actividads", "ID", cascadeDelete: true);
        }
    }
}
