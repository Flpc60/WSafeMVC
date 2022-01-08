namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegrateIncidente1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Riesgoes", "Incidente_ID", "dbo.Incidentes");
            DropIndex("dbo.Riesgoes", new[] { "Incidente_ID" });
            AddColumn("dbo.Incidentes", "Riesgo_ID", c => c.Int());
            CreateIndex("dbo.Incidentes", "Riesgo_ID");
            AddForeignKey("dbo.Incidentes", "Riesgo_ID", "dbo.Riesgoes", "ID");
            DropColumn("dbo.Riesgoes", "Incidente_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riesgoes", "Incidente_ID", c => c.Int());
            DropForeignKey("dbo.Incidentes", "Riesgo_ID", "dbo.Riesgoes");
            DropIndex("dbo.Incidentes", new[] { "Riesgo_ID" });
            DropColumn("dbo.Incidentes", "Riesgo_ID");
            CreateIndex("dbo.Riesgoes", "Incidente_ID");
            AddForeignKey("dbo.Riesgoes", "Incidente_ID", "dbo.Incidentes", "ID");
        }
    }
}
