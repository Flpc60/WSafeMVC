namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRootCause1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reasons", "RootCauseID", c => c.Int(nullable: false));
            DropColumn("dbo.Reasons", "EventID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reasons", "EventID", c => c.Int(nullable: false));
            DropColumn("dbo.Reasons", "RootCauseID");
        }
    }
}
