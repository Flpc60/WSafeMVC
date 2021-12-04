namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaRiesgo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Controls", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "CategoriaAplicacion", c => c.Int(nullable: false));
            AlterColumn("dbo.Controls", "Codigo", c => c.String());
            DropColumn("dbo.Aplicacions", "CategoriaAplicacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Aplicacions", "CategoriaAplicacion", c => c.Int(nullable: false));
            AlterColumn("dbo.Controls", "Codigo", c => c.String(nullable: false));
            DropColumn("dbo.Controls", "CategoriaAplicacion");
            DropColumn("dbo.Controls", "RiesgoID");
        }
    }
}
