namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "FechaResolucionLicencia", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Organizations", "FechaRenovacionLicencia", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Organizations", "FechaRenovacionCurso", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "FechaRenovacionCurso");
            DropColumn("dbo.Organizations", "FechaRenovacionLicencia");
            DropColumn("dbo.Organizations", "FechaResolucionLicencia");
        }
    }
}
