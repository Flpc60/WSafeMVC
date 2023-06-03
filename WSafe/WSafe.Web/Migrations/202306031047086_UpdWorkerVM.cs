namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorkerVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkersVMs", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkersVMs", "Email", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
