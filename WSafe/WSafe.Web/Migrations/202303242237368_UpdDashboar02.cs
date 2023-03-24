namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdDashboar02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "FechaIngreso", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "FechaRetiro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "OrganizationID", c => c.Int(nullable: false));
            DropColumn("dbo.Trabajadors", "FechaNomina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "FechaNomina", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trabajadors", "OrganizationID");
            DropColumn("dbo.Trabajadors", "FechaRetiro");
            DropColumn("dbo.Trabajadors", "FechaIngreso");
        }
    }
}
