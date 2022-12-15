namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "ResponsableSGSST", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "ResponsableSGSST");
        }
    }
}
