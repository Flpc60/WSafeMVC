namespace WSafe.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class refinarRiesgo11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Controls", "AplicacionID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "CategoriaEfectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Trazas", "AplicacionID", c => c.Int(nullable: false));
            AddColumn("dbo.Trazas", "RiesgoolID", c => c.Int(nullable: false));
            DropColumn("dbo.Trazas", "RiesgoID");
            DropColumn("dbo.Aplicacions", "CategoriaEfectividad");
        }

        public override void Down()
        {
            AddColumn("dbo.Aplicacions", "CategoriaEfectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Trazas", "RiesgoID", c => c.Int(nullable: false));
            DropColumn("dbo.Trazas", "RiesgoolID");
            DropColumn("dbo.Trazas", "AplicacionID");
            DropColumn("dbo.Controls", "CategoriaEfectividad");
            DropColumn("dbo.Controls", "AplicacionID");
        }
    }
}
