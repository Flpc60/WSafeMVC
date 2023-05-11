namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdAccion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accions", "ClientID");
            DropColumn("dbo.Accions", "OrganizationID");
        }
    }
}
