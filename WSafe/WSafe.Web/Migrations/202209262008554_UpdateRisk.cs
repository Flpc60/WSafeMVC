namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRisk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "DangerCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Riesgoes", "DangerCategory");
        }
    }
}
