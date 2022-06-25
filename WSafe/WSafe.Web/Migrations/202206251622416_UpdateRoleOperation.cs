namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoleOperation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleOperations", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.RoleOperations", "Operation", c => c.Int(nullable: false));
            AddColumn("dbo.RoleOperations", "Component", c => c.Int(nullable: false));
            DropColumn("dbo.RoleOperations", "RolID");
            DropColumn("dbo.RoleOperations", "OperationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleOperations", "OperationID", c => c.Int(nullable: false));
            AddColumn("dbo.RoleOperations", "RolID", c => c.Int(nullable: false));
            DropColumn("dbo.RoleOperations", "Component");
            DropColumn("dbo.RoleOperations", "Operation");
            DropColumn("dbo.RoleOperations", "RoleID");
        }
    }
}
