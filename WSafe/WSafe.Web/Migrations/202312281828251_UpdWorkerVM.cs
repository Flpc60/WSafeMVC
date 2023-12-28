namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorkerVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "FreeTime", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "IngresoCategory", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "FreeTime", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "IngresoCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkersVMs", "IngresoCategory");
            DropColumn("dbo.WorkersVMs", "FreeTime");
            DropColumn("dbo.Trabajadors", "IngresoCategory");
            DropColumn("dbo.Trabajadors", "FreeTime");
        }
    }
}
