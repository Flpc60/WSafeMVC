namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdCrtVylnerabilityVM1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CrtVulnerabilityVMs", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CrtVulnerabilityVMs", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
