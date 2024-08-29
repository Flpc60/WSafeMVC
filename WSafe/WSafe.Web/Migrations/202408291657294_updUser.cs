namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResponsableSgsst", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ResponsableSgsst");
        }
    }
}
