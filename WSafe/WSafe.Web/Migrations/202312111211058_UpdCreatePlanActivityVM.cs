namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdCreatePlanActivityVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "DateSigue", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "DateSigue", c => c.String(nullable: false));
        }
    }
}
