namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegracionIncidente3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiesgoViewModels", "IncidenteID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "IncidenteID");
        }
    }
}
