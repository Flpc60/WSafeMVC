namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicatorsImplementation111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicadors", "TipoChart", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicadors", "TipoChart");
        }
    }
}
