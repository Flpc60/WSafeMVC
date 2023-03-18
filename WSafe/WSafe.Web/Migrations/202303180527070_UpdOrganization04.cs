namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOrganization04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "ControlDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "ControlDate");
        }
    }
}
