namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimients", "Path", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimients", "Path");
        }
    }
}
