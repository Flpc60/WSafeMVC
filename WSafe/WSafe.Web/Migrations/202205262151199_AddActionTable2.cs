namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionTable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZonaID = c.Int(nullable: false),
                        ProcesoID = c.Int(nullable: false),
                        ActividadID = c.Int(nullable: false),
                        TareaID = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        Categoria = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        FuenteAccion = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        EficaciaAntes = c.Int(nullable: false),
                        EficaciaDespues = c.Int(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        Efectiva = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        RiesgoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
        }
    }
}