namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRoleUserVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleUserVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.RoleUserVMs", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleUserVMs", "ClientID");
            DropColumn("dbo.RoleUserVMs", "OrganizationID");
        }
    }
}
