namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimients", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimients", "UserID");
        }
    }
}
