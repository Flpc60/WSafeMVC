namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdAccionVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccionViewModels", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccionViewModels", "ClientID");
            DropColumn("dbo.AccionViewModels", "OrganizationID");
        }
    }
}
