namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardCapacitation", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardCapacitation");
        }
    }
}
