namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorker : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trabajadors", "Profesion", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Profesion", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkersVMs", "Profesion", c => c.String());
            AlterColumn("dbo.Trabajadors", "Profesion", c => c.String());
        }
    }
}
