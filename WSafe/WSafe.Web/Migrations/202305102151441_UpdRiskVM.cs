namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdRiskVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiesgoViewModels", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "ClientID");
            DropColumn("dbo.RiesgoViewModels", "OrganizationID");
        }
    }
}
