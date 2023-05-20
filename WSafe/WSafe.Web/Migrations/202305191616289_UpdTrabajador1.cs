namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrabajador1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "DocumentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trabajadors", "DocumentType");
            DropColumn("dbo.Trabajadors", "UserID");
        }
    }
}
