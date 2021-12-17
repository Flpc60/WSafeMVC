namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "NivelRiesgo", c => c.Int(nullable: false));
            AddColumn("dbo.Riesgoes", "CategoriaRiesgo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Riesgoes", "CategoriaRiesgo");
            DropColumn("dbo.Riesgoes", "NivelRiesgo");
        }
    }
}
