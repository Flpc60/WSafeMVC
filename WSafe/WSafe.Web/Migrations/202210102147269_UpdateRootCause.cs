namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRootCause : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BarrierAnalice", "IncidentID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BarrierAnalice", "IncidentID");
        }
    }
}
