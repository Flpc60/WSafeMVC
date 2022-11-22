namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientVMs", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientVMs", "UserID");
        }
    }
}
