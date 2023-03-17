namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardIncidents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardIncidents");
        }
    }
}
