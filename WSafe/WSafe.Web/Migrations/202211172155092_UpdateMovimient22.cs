namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimients", "Type", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.MovimientVMs", "Type", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientVMs", "Type");
            DropColumn("dbo.Movimients", "Type");
        }
    }
}
