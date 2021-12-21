namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinaRiesgo8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropColumn("dbo.Trabajadors", "Foto");
            DropColumn("dbo.Trabajadors", "Firma");
            DropColumn("dbo.Trabajadors", "Email");
            DropColumn("dbo.Trabajadors", "TipoVinculacion");
            DropColumn("dbo.Trabajadors", "FechaIngreso");
            DropColumn("dbo.Trabajadors", "EPS");
            DropColumn("dbo.Trabajadors", "AFP");
            DropColumn("dbo.Trabajadors", "ARL");
            DropColumn("dbo.Trabajadors", "Direccion");
            DropColumn("dbo.Trabajadors", "Telefonos");
            DropColumn("dbo.Trabajadors", "AltoRiesgo");
            DropColumn("dbo.Trabajadors", "ActividadAltoRiesgo");
            DropColumn("dbo.Trabajadors", "Cargo_ID");
            DropTable("dbo.Cargoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "ActividadAltoRiesgo", c => c.String());
            AddColumn("dbo.Trabajadors", "AltoRiesgo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trabajadors", "Telefonos", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "ARL", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "AFP", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "EPS", c => c.String(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaIngreso", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "TipoVinculacion", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Email", c => c.String());
            AddColumn("dbo.Trabajadors", "Firma", c => c.String());
            AddColumn("dbo.Trabajadors", "Foto", c => c.String());
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID", cascadeDelete: true);
        }
    }
}
