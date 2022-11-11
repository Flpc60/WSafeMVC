namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimients", "Descripcion", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Movimients", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movimients", "Name", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Movimients", "Descripcion");
        }
    }
}
