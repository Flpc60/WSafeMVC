namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegracionIncidente2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Incidentes", "Riesgo_ID", "dbo.Riesgoes");
            DropIndex("dbo.Incidentes", new[] { "Riesgo_ID" });
            AddColumn("dbo.Riesgoes", "IncidenteID", c => c.Int(nullable: false));
            DropColumn("dbo.Incidentes", "Riesgo_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incidentes", "Riesgo_ID", c => c.Int());
            DropColumn("dbo.Riesgoes", "IncidenteID");
            CreateIndex("dbo.Incidentes", "Riesgo_ID");
            AddForeignKey("dbo.Incidentes", "Riesgo_ID", "dbo.Riesgoes", "ID");
        }
    }
}
