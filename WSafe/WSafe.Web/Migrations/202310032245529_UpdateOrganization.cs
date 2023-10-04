namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardRecomendations", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardRecomendations");
        }
    }
}
