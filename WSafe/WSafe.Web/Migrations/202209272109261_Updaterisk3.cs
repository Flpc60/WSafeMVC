namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updaterisk3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiesgoViewModels", "DangerCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "DangerCategory");
        }
    }
}
