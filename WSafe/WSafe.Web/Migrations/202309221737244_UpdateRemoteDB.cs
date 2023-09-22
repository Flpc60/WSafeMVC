namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRemoteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionsMatrixVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaSolicitud = c.String(),
                        Zona = c.String(),
                        Proceso = c.String(),
                        Actividad = c.String(),
                        FuenteAccion = c.String(),
                        Categoria = c.String(),
                        Descripcion = c.String(maxLength: 100),
                        CategoriaCausa = c.String(),
                        Planes = c.String(),
                        FechaCierre = c.String(),
                        Responsable = c.String(),
                        ActionState = c.String(),
                        Efectiva = c.Boolean(nullable: false),
                        Eficacia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Accions", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Actividads", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Actividads", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Cargoes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Cargoes", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Indicadors", "Standard", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Organizations", "StandardIndicators", c => c.Int(nullable: false));
            AddColumn("dbo.Procesoes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Procesoes", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Tareas", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Tareas", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Activo", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkersVMs", "Activo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Zonas", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Zonas", "ClientID", c => c.Int(nullable: false));
            AlterColumn("dbo.Indicadors", "TipoChart", c => c.String(nullable: false));
            AlterColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "FuenteControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "MedioControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "IndividuoControls", c => c.String(maxLength: 300));
            DropColumn("dbo.Indicadors", "Fuente");
            DropColumn("dbo.Indicadors", "Tipo");
            DropColumn("dbo.Indicadors", "Responsable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicadors", "Responsable", c => c.String());
            AddColumn("dbo.Indicadors", "Tipo", c => c.String());
            AddColumn("dbo.Indicadors", "Fuente", c => c.String());
            AlterColumn("dbo.RiesgoViewModels", "IndividuoControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.RiesgoViewModels", "MedioControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.RiesgoViewModels", "FuenteControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Indicadors", "TipoChart", c => c.String());
            DropColumn("dbo.Zonas", "ClientID");
            DropColumn("dbo.Zonas", "OrganizationID");
            DropColumn("dbo.WorkersVMs", "Activo");
            DropColumn("dbo.Trabajadors", "Activo");
            DropColumn("dbo.Tareas", "ClientID");
            DropColumn("dbo.Tareas", "OrganizationID");
            DropColumn("dbo.RiesgoViewModels", "UserID");
            DropColumn("dbo.Riesgoes", "UserID");
            DropColumn("dbo.Procesoes", "ClientID");
            DropColumn("dbo.Procesoes", "OrganizationID");
            DropColumn("dbo.Organizations", "StandardIndicators");
            DropColumn("dbo.Indicadors", "Standard");
            DropColumn("dbo.IncidenteViewModels", "UserID");
            DropColumn("dbo.Incidentes", "UserID");
            DropColumn("dbo.Cargoes", "ClientID");
            DropColumn("dbo.Cargoes", "OrganizationID");
            DropColumn("dbo.Actividads", "ClientID");
            DropColumn("dbo.Actividads", "OrganizationID");
            DropColumn("dbo.AccionViewModels", "UserID");
            DropColumn("dbo.Accions", "UserID");
            DropTable("dbo.ActionsMatrixVMs");
        }
    }
}
