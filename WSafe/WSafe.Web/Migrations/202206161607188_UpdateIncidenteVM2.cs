namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIncidenteVM2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncidenteViewModels", "FechaIncidentStr", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncidenteViewModels", "FechaIncidentStr", c => c.String(nullable: false));
        }
    }
}
