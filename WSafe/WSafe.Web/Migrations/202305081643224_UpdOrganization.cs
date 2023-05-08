namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOrganization : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "FechaResolucionLicencia", c => c.String(maxLength: 12));
            AlterColumn("dbo.Organizations", "FechaRenovacionLicencia", c => c.String(maxLength: 12));
            AlterColumn("dbo.Organizations", "FechaRenovacionCurso", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "FechaRenovacionCurso", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Organizations", "FechaRenovacionLicencia", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Organizations", "FechaResolucionLicencia", c => c.String(nullable: false, maxLength: 12));
        }
    }
}
