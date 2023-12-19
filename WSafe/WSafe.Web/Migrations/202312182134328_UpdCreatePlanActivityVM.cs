namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdCreatePlanActivityVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "Entregables", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreatePlanActivityVMs", "Entregables", c => c.String(maxLength: 100));
        }
    }
}
