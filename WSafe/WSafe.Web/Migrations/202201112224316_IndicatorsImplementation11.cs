namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicatorsImplementation11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndicadorViewModels", "Imagen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndicadorViewModels", "Imagen");
        }
    }
}
