namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccidente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accidentadoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrabajadorID = c.Int(nullable: false),
                        IncidenteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Trabajadors", "Incidente_ID", c => c.Int());
            AddColumn("dbo.Incidentes", "Informante", c => c.String(nullable: false));
            AddColumn("dbo.IncidenteViewModels", "Informante", c => c.String(nullable: false));
            CreateIndex("dbo.Trabajadors", "Incidente_ID");
            AddForeignKey("dbo.Trabajadors", "Incidente_ID", "dbo.Incidentes", "ID");
            DropColumn("dbo.Incidentes", "TrabajadorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incidentes", "TrabajadorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Trabajadors", "Incidente_ID", "dbo.Incidentes");
            DropIndex("dbo.Trabajadors", new[] { "Incidente_ID" });
            DropColumn("dbo.IncidenteViewModels", "Informante");
            DropColumn("dbo.Incidentes", "Informante");
            DropColumn("dbo.Trabajadors", "Incidente_ID");
            DropTable("dbo.Accidentadoes");
        }
    }
}
