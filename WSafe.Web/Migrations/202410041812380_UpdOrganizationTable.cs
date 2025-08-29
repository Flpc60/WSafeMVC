namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOrganizationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardEmergenciesPLan", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardEmergenciesPLan");
        }
    }
}
