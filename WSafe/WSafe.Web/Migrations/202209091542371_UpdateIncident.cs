namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIncident : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidentes", "TxtFechaIncident", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incidentes", "TxtFechaIncident");
        }
    }
}
