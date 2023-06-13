namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpUnsafeactVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Unsafeacts", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.UnsafeactVMs", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnsafeactVMs", "UserID");
            DropColumn("dbo.Unsafeacts", "UserID");
        }
    }
}
