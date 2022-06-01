namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeguimientoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeguimientoAccions",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    AccionID = c.Int(nullable: false),
                    Resultado = c.String(nullable: false),
                    FechaSeguimiento = c.DateTime(nullable: false),
                    TrabajadorID = c.Int(nullable: false),
                    Responsable = c.String(nullable: false)
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accions", t => t.AccionID, cascadeDelete: true)
                .Index(t => t.AccionID);
        }
        public override void Down()
        {
        }
    }
}