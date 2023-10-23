namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auditers", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Auditers", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auditers", "ClientID");
            DropColumn("dbo.Auditers", "OrganizationID");
        }
    }
}
