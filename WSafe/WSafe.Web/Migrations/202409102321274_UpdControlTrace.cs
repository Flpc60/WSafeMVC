namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdControlTrace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ControlTraces", "GenerateAction", c => c.Boolean(nullable: false));
            AddColumn("dbo.ControlTraces", "Finality", c => c.Int(nullable: false));
            AddColumn("dbo.ControlTraces", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.ControlTraces", "AplicationCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ControlTraces", "AplicationCategory");
            DropColumn("dbo.ControlTraces", "TrabajadorID");
            DropColumn("dbo.ControlTraces", "Finality");
            DropColumn("dbo.ControlTraces", "GenerateAction");
        }
    }
}
