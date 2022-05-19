namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndicadorVM1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndicadorViewModels", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.IndicadorViewModels", "Periodo", c => c.String());
            DropColumn("dbo.IndicadorViewModels", "FechaInicial");
            DropColumn("dbo.IndicadorViewModels", "FechaFinal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndicadorViewModels", "FechaFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.IndicadorViewModels", "FechaInicial", c => c.DateTime(nullable: false));
            DropColumn("dbo.IndicadorViewModels", "Periodo");
            DropColumn("dbo.IndicadorViewModels", "Year");
        }
    }
}
