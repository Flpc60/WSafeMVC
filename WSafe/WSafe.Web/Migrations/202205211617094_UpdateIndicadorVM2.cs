namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndicadorVM2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndicadorViewModels", "Titulo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndicadorViewModels", "Titulo");
        }
    }
}
