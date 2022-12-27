namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "DocumentResponsable", c => c.String(maxLength: 20));
            AddColumn("dbo.Organizations", "ResolucionLicencia", c => c.DateTime(nullable: false));
            AddColumn("dbo.Organizations", "ResponsableLicencia", c => c.String(maxLength: 20));
            AddColumn("dbo.Organizations", "RenovacionLicencia", c => c.DateTime(nullable: false));
            AddColumn("dbo.Organizations", "RenovacionCurso", c => c.DateTime(nullable: false));
            AddColumn("dbo.Organizations", "NivelEstudios", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "MesesExperiencia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "MesesExperiencia");
            DropColumn("dbo.Organizations", "NivelEstudios");
            DropColumn("dbo.Organizations", "RenovacionCurso");
            DropColumn("dbo.Organizations", "RenovacionLicencia");
            DropColumn("dbo.Organizations", "ResponsableLicencia");
            DropColumn("dbo.Organizations", "ResolucionLicencia");
            DropColumn("dbo.Organizations", "DocumentResponsable");
        }
    }
}
