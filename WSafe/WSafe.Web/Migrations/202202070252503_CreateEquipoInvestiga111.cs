namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEquipoInvestiga111 : DbMigration
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
            
            CreateIndex("dbo.Accidentadoes", "TrabajadorID");
            CreateIndex("dbo.Accidentadoes", "IncidenteID");
            AddForeignKey("dbo.Accidentadoes", "TrabajadorID", "dbo.Trabajadors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Accidentadoes", "IncidenteID", "dbo.Incidentes", "ID", cascadeDelete: true);
            DropColumn("dbo.Trabajadors", "Incidente_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "Incidente_ID", c => c.Int());
            DropForeignKey("dbo.Accidentadoes", "IncidenteID", "dbo.Incidentes");
            DropForeignKey("dbo.Accidentadoes", "TrabajadorID", "dbo.Trabajadors");
            DropIndex("dbo.Accidentadoes", new[] { "IncidenteID" });
            DropIndex("dbo.Accidentadoes", new[] { "TrabajadorID" });
            DropTable("dbo.Cargoes");
            CreateIndex("dbo.Trabajadors", "Incidente_ID");
            AddForeignKey("dbo.Trabajadors", "Incidente_ID", "dbo.Incidentes", "ID");
        }
    }
}
