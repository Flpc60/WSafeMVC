namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdSiguePlanAnual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiguePlanAnuals", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.SiguePlanAnuals", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.SiguePlanAnuals", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiguePlanAnuals", "UserID");
            DropColumn("dbo.SiguePlanAnuals", "ClientID");
            DropColumn("dbo.SiguePlanAnuals", "OrganizationID");
        }
    }
}
