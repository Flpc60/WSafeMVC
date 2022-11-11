namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimients", "Year", c => c.String(nullable: false, maxLength: 4));
            AddColumn("dbo.Movimients", "Item", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.Movimients", "Ciclo", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.Organizations", "Year", c => c.String(nullable: false, maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "Year");
            DropColumn("dbo.Movimients", "Ciclo");
            DropColumn("dbo.Movimients", "Item");
            DropColumn("dbo.Movimients", "Year");
        }
    }
}
