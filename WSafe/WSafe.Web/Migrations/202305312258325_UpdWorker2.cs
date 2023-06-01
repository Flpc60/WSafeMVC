namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorker2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "Tratamiento", c => c.String());
            DropColumn("dbo.Trabajadors", "Tratamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "Tratamento", c => c.String());
            DropColumn("dbo.Trabajadors", "Tratamiento");
        }
    }
}
