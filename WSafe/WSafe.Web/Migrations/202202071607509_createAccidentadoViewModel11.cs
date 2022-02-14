namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createAccidentadoViewModel11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trabajadors", "Incidente_ID", "dbo.Incidentes");
            DropIndex("dbo.Trabajadors", new[] { "Incidente_ID" });
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int());
            CreateIndex("dbo.Accidentadoes", "IncidenteID");
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID");
            AddForeignKey("dbo.Accidentadoes", "IncidenteID", "dbo.Incidentes", "ID", cascadeDelete: true);
            DropColumn("dbo.Trabajadors", "Incidente_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "Incidente_ID", c => c.Int());
            DropForeignKey("dbo.Accidentadoes", "IncidenteID", "dbo.Incidentes");
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropIndex("dbo.Accidentadoes", new[] { "IncidenteID" });
            DropColumn("dbo.Trabajadors", "Cargo_ID");
            DropTable("dbo.Cargoes");
            CreateIndex("dbo.Trabajadors", "Incidente_ID");
            AddForeignKey("dbo.Trabajadors", "Incidente_ID", "dbo.Incidentes", "ID");
        }
    }
}
