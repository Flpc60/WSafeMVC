namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSiguePlanAnual1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SiguePlanAnuals", "FileName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SiguePlanAnuals", "FileName", c => c.String());
        }
    }
}
