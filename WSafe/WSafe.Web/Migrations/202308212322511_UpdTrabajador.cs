namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrabajador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "Activo", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkersVMs", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkersVMs", "Activo");
            DropColumn("dbo.Trabajadors", "Activo");
        }
    }
}
