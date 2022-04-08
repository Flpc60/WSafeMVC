namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeguimiento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo._DetailsAccionVM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Formato = c.String(),
                        Estandar = c.String(),
                        Titulo = c.String(),
                        Fecha = c.String(),
                        Version = c.String(),
                        FechaSolicitud = c.String(),
                        Categoria = c.Int(nullable: false),
                        Responsable = c.String(),
                        Cargo = c.String(),
                        Proceso = c.String(),
                        FuenteAccion = c.String(),
                        Descripcion = c.String(),
                        EficaciaAntes = c.Int(nullable: false),
                        EficaciaDespues = c.Int(nullable: false),
                        FechaCierre = c.String(),
                        Efectiva = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PlanAccions", "Responsable", c => c.String());
            AddColumn("dbo.PlanAccions", "_DetailsAccionVM_ID", c => c.Int());
            AddColumn("dbo.SeguimientoAccions", "Responsable", c => c.String());
            AddColumn("dbo.SeguimientoAccions", "_DetailsAccionVM_ID", c => c.Int());
            CreateIndex("dbo.PlanAccions", "_DetailsAccionVM_ID");
            CreateIndex("dbo.SeguimientoAccions", "_DetailsAccionVM_ID");
            AddForeignKey("dbo.PlanAccions", "_DetailsAccionVM_ID", "dbo._DetailsAccionVM", "ID");
            AddForeignKey("dbo.SeguimientoAccions", "_DetailsAccionVM_ID", "dbo._DetailsAccionVM", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeguimientoAccions", "_DetailsAccionVM_ID", "dbo._DetailsAccionVM");
            DropForeignKey("dbo.PlanAccions", "_DetailsAccionVM_ID", "dbo._DetailsAccionVM");
            DropIndex("dbo.SeguimientoAccions", new[] { "_DetailsAccionVM_ID" });
            DropIndex("dbo.PlanAccions", new[] { "_DetailsAccionVM_ID" });
            DropColumn("dbo.SeguimientoAccions", "_DetailsAccionVM_ID");
            DropColumn("dbo.SeguimientoAccions", "Responsable");
            DropColumn("dbo.PlanAccions", "_DetailsAccionVM_ID");
            DropColumn("dbo.PlanAccions", "Responsable");
            DropTable("dbo._DetailsAccionVM");
        }
    }
}
