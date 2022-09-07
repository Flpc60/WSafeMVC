namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "Politica", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "Politica");
        }
    }
}
