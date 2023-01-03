namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma33 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlanActivities", "NormaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "NormaID", c => c.Int(nullable: false));
        }
    }
}
