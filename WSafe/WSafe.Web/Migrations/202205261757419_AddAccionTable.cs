namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accions",
                c => new
                {

                    ID = c.Int(nullable: false, identity: true),
                    RiesgoID = c.Int(nullable: false),
                    Categoria = c.Int(nullable: false),
                    FechaSolicitud = c.DateTime(nullable: false),
                    Intervenciones = c.String(nullable: false),
                    Observaciones = c.String(nullable: false),
                    FechaInicial = c.DateTime(nullable: false),
                    FechaFinal = c.DateTime(nullable: false),
                    MedidasControl = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Riesgoes", t => t.RiesgoID, cascadeDelete: true)
                .Index(t => t.RiesgoID);
        }
        
        public override void Down()
        {
        }
    }
}
