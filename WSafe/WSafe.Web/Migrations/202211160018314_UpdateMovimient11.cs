namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movimients", "Document", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.MovimientVMs", "Document", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovimientVMs", "Document", c => c.Binary());
            AlterColumn("dbo.Movimients", "Document", c => c.Binary());
        }
    }
}
