namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdAplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aplicacions", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AplicacionVMs", "UserID");
            DropColumn("dbo.AplicacionVMs", "ClientID");
            DropColumn("dbo.AplicacionVMs", "OrganizationID");
            DropColumn("dbo.Aplicacions", "UserID");
            DropColumn("dbo.Aplicacions", "ClientID");
            DropColumn("dbo.Aplicacions", "OrganizationID");
        }
    }
}
