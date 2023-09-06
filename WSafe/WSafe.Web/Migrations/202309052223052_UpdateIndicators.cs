namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndicators : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicadors", "Standard", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Indicadors", "TipoChart", c => c.String(nullable: false));
            DropColumn("dbo.Indicadors", "Fuente");
            DropColumn("dbo.Indicadors", "Tipo");
            DropColumn("dbo.Indicadors", "Responsable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicadors", "Responsable", c => c.String());
            AddColumn("dbo.Indicadors", "Tipo", c => c.String());
            AddColumn("dbo.Indicadors", "Fuente", c => c.String());
            AlterColumn("dbo.Indicadors", "TipoChart", c => c.String());
            DropColumn("dbo.Indicadors", "Standard");
        }
    }
}
