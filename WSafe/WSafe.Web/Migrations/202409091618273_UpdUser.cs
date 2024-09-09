namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Firma", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Firma");
        }
    }
}
