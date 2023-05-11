namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrabajador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trabajadors", "ClientID");
        }
    }
}
