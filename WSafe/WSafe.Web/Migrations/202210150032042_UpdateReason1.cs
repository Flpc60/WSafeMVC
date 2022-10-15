namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReason1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Reasons", "RootCauseID");
            AddForeignKey("dbo.Reasons", "RootCauseID", "dbo.RootCauses", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reasons", "RootCauseID", "dbo.RootCauses");
            DropIndex("dbo.Reasons", new[] { "RootCauseID" });
        }
    }
}
