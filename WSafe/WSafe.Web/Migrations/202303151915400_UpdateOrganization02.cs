namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "NIT", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Organizations", "ARL", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "ARL", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Organizations", "NIT", c => c.String(nullable: false));
        }
    }
}
