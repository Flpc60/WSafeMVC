namespace WSafe.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarTablas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            AddColumn("dbo.Peligroes", "CategoriaPeligro_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Trabajadors", "PrimerApellido", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "PrimerNombre", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Documento", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Firma", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Telefonos", c => c.String(nullable: false));
            AlterColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Peligroes", "CategoriaPeligro_ID");
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            AddForeignKey("dbo.Peligroes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID", cascadeDelete: true);
            DropColumn("dbo.Peligroes", "CategoriaPeligroID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Peligroes", "CategoriaPeligroID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropForeignKey("dbo.Peligroes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes");
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            DropIndex("dbo.Peligroes", new[] { "CategoriaPeligro_ID" });
            AlterColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int());
            AlterColumn("dbo.Trabajadors", "Telefonos", c => c.String());
            AlterColumn("dbo.Trabajadors", "Direccion", c => c.String());
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String());
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String());
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String());
            AlterColumn("dbo.Trabajadors", "Firma", c => c.String());
            AlterColumn("dbo.Trabajadors", "Documento", c => c.String());
            AlterColumn("dbo.Trabajadors", "PrimerNombre", c => c.String());
            AlterColumn("dbo.Trabajadors", "PrimerApellido", c => c.String());
            DropColumn("dbo.Peligroes", "CategoriaPeligro_ID");
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID");
        }
    }
}
