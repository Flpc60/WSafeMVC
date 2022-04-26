namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgoVM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors");
            DropIndex("dbo.Aplicacions", new[] { "Trabajador_ID" });
            AddColumn("dbo.Aplicacions", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "Responsable", c => c.String());
            AddColumn("dbo.AplicacionVMs", "TextFechaInicial", c => c.String());
            AddColumn("dbo.AplicacionVMs", "TextFechaFinal", c => c.String());
            DropColumn("dbo.Aplicacions", "Trabajador_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Aplicacions", "Trabajador_ID", c => c.Int(nullable: false));
            DropColumn("dbo.AplicacionVMs", "TextFechaFinal");
            DropColumn("dbo.AplicacionVMs", "TextFechaInicial");
            DropColumn("dbo.AplicacionVMs", "Responsable");
            DropColumn("dbo.Aplicacions", "TrabajadorID");
            CreateIndex("dbo.Aplicacions", "Trabajador_ID");
            AddForeignKey("dbo.Aplicacions", "Trabajador_ID", "dbo.Trabajadors", "ID", cascadeDelete: true);
        }
    }
}
