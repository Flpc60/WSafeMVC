namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinaRiesgo5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Controls", "Codigo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Controls", "Codigo", c => c.String());
        }
    }
}
