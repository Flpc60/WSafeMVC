namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refinarRiesgo10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiesgoViewModels", "CategoriaRiesgo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "CategoriaRiesgo");
        }
    }
}
