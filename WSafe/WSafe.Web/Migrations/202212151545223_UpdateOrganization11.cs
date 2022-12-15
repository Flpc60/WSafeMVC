namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "Agropecuaria", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "Agropecuaria");
        }
    }
}
