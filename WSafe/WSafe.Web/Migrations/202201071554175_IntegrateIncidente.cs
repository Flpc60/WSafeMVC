namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegrateIncidente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "Incidente_ID", c => c.Int());
            CreateIndex("dbo.Riesgoes", "Incidente_ID");
            AddForeignKey("dbo.Riesgoes", "Incidente_ID", "dbo.Incidentes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Riesgoes", "Incidente_ID", "dbo.Incidentes");
            DropIndex("dbo.Riesgoes", new[] { "Incidente_ID" });
            DropColumn("dbo.Riesgoes", "Incidente_ID");
        }
    }
}
