namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DocumentType = c.Int(nullable: false),
                        Document = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(),
                        Folders = c.Int(nullable: false),
                        ControlDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Incidentes", "DiasPerdidos", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "DiasCargados", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "CategoriaPeligroID", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "TipoEvento", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "TipoLesion", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "TipoAgente", c => c.Int(nullable: false));
            AddColumn("dbo.Incidentes", "CategoriaAfectacion", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "DocumentType", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ClientID");
            DropColumn("dbo.Users", "OrganizationID");
            DropColumn("dbo.Organizations", "ClientID");
            DropColumn("dbo.Organizations", "DocumentType");
            DropColumn("dbo.Incidentes", "CategoriaAfectacion");
            DropColumn("dbo.Incidentes", "TipoAgente");
            DropColumn("dbo.Incidentes", "TipoLesion");
            DropColumn("dbo.Incidentes", "TipoEvento");
            DropColumn("dbo.Incidentes", "CategoriaPeligroID");
            DropColumn("dbo.Incidentes", "DiasCargados");
            DropColumn("dbo.Incidentes", "DiasPerdidos");
            DropTable("dbo.Clients");
        }
    }
}
