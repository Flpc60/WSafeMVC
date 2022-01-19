namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinamientoRiesgo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls");
            DropIndex("dbo.Aplicacions", new[] { "Control_ID" });
            AddColumn("dbo.Aplicacions", "Nombre", c => c.String());
            AddColumn("dbo.Aplicacions", "CategoriaAplicacion", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Finalidad", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Intervencion", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "Beneficios", c => c.String());
            AlterColumn("dbo.Aplicacions", "Observaciones", c => c.String(nullable: false));
            DropColumn("dbo.Aplicacions", "Control_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Aplicacions", "Control_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Aplicacions", "Observaciones", c => c.String());
            DropColumn("dbo.Aplicacions", "Beneficios");
            DropColumn("dbo.Aplicacions", "Intervencion");
            DropColumn("dbo.Aplicacions", "Finalidad");
            DropColumn("dbo.Aplicacions", "CategoriaAplicacion");
            DropColumn("dbo.Aplicacions", "Nombre");
            CreateIndex("dbo.Aplicacions", "Control_ID");
            AddForeignKey("dbo.Aplicacions", "Control_ID", "dbo.Controls", "ID", cascadeDelete: true);
        }
    }
}
