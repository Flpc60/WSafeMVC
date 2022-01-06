namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatenewIncidente111111111111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidenteViewModels", "TrabajadorID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidenteViewModels", "TrabajadorID");
        }
    }
}
