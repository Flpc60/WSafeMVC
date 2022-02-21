namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccionesVM11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Causa = c.Int(nullable: false),
                        Accion = c.String(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Prioritaria = c.Boolean(nullable: false),
                        Costos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccionViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccionViewModels", t => t.AccionViewModel_ID)
                .Index(t => t.AccionViewModel_ID);
            
            CreateTable(
                "dbo.SeguimientoAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        AccionViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccionViewModels", t => t.AccionViewModel_ID)
                .Index(t => t.AccionViewModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeguimientoAccions", "AccionViewModel_ID", "dbo.AccionViewModels");
            DropForeignKey("dbo.PlanAccions", "AccionViewModel_ID", "dbo.AccionViewModels");
            DropIndex("dbo.SeguimientoAccions", new[] { "AccionViewModel_ID" });
            DropIndex("dbo.PlanAccions", new[] { "AccionViewModel_ID" });
            DropTable("dbo.SeguimientoAccions");
            DropTable("dbo.PlanAccions");
        }
    }
}
