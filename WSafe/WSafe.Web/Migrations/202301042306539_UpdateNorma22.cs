namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivities", "Ciclo", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.PlanActivities", "Item", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.PlanActivities", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanActivities", "Name");
            DropColumn("dbo.PlanActivities", "Item");
            DropColumn("dbo.PlanActivities", "Ciclo");
        }
    }
}
