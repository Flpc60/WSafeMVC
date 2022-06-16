namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "Products", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Organizations", "Mision", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Organizations", "Vision", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "Vision", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Organizations", "Mision", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Organizations", "Products", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
