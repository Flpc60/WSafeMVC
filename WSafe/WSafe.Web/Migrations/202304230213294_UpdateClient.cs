namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Phone", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Phone", c => c.String());
        }
    }
}
