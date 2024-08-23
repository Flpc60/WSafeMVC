namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdActionTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlanActions", "Accion", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlanActions", "Accion", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
