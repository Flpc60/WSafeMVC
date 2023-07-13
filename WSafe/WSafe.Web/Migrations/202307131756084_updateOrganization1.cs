namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrganization1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Actividads", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Actividads", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Cargoes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Cargoes", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Procesoes", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Procesoes", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Tareas", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Tareas", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Zonas", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Zonas", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zonas", "ClientID");
            DropColumn("dbo.Zonas", "OrganizationID");
            DropColumn("dbo.Tareas", "ClientID");
            DropColumn("dbo.Tareas", "OrganizationID");
            DropColumn("dbo.Riesgoes", "UserID");
            DropColumn("dbo.Procesoes", "ClientID");
            DropColumn("dbo.Procesoes", "OrganizationID");
            DropColumn("dbo.Incidentes", "UserID");
            DropColumn("dbo.Cargoes", "ClientID");
            DropColumn("dbo.Cargoes", "OrganizationID");
            DropColumn("dbo.Actividads", "ClientID");
            DropColumn("dbo.Actividads", "OrganizationID");
            DropColumn("dbo.Accions", "UserID");
        }
    }
}
