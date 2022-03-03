namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditor3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditRiesgoes", "Fecha", c => c.DateTime(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "Descripcion", c => c.String(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "Usuario", c => c.String(nullable: false));
            DropColumn("dbo.AuditRiesgoes", "ZonaID");
            DropColumn("dbo.AuditRiesgoes", "ProcesoID");
            DropColumn("dbo.AuditRiesgoes", "ActividadID");
            DropColumn("dbo.AuditRiesgoes", "TareaID");
            DropColumn("dbo.AuditRiesgoes", "Rutinaria");
            DropColumn("dbo.AuditRiesgoes", "PeligroID");
            DropColumn("dbo.AuditRiesgoes", "EfectosPosibles");
            DropColumn("dbo.AuditRiesgoes", "NivelDeficiencia");
            DropColumn("dbo.AuditRiesgoes", "NivelExposicion");
            DropColumn("dbo.AuditRiesgoes", "NivelProbabilidad");
            DropColumn("dbo.AuditRiesgoes", "NivelConsecuencia");
            DropColumn("dbo.AuditRiesgoes", "NivelRiesgo");
            DropColumn("dbo.AuditRiesgoes", "Aceptabilidad");
            DropColumn("dbo.AuditRiesgoes", "NroExpuestos");
            DropColumn("dbo.AuditRiesgoes", "RequisitoLegal");
            DropColumn("dbo.AuditRiesgoes", "Operation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditRiesgoes", "Operation", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "RequisitoLegal", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NroExpuestos", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "Aceptabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NivelConsecuencia", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NivelProbabilidad", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NivelExposicion", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "NivelDeficiencia", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "EfectosPosibles", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "PeligroID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "Rutinaria", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "TareaID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "ActividadID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "ProcesoID", c => c.Int(nullable: false));
            AddColumn("dbo.AuditRiesgoes", "ZonaID", c => c.Int(nullable: false));
            DropColumn("dbo.AuditRiesgoes", "Usuario");
            DropColumn("dbo.AuditRiesgoes", "Descripcion");
            DropColumn("dbo.AuditRiesgoes", "Fecha");
        }
    }
}
