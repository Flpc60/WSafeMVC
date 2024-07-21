namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrtControlTrace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainCauses", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.MainCauses", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.MainCauses", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainCauses", "UserID");
            DropColumn("dbo.MainCauses", "ClientID");
            DropColumn("dbo.MainCauses", "OrganizationID");
        }
    }
}
