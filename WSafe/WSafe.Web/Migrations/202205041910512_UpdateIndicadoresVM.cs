namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndicadoresVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicadors", "Meta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Indicadors", "Fuente", c => c.String());
            AddColumn("dbo.Indicadors", "Tipo", c => c.String());
            AddColumn("dbo.Indicadors", "Responsable", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicadors", "Responsable");
            DropColumn("dbo.Indicadors", "Tipo");
            DropColumn("dbo.Indicadors", "Fuente");
            DropColumn("dbo.Indicadors", "Meta");
        }
    }
}
