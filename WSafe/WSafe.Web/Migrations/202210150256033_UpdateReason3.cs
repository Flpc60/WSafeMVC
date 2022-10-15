namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReason3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reasons", "RootCauseID", "dbo.RootCauses");
            DropIndex("dbo.Reasons", new[] { "RootCauseID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Reasons", "RootCauseID");
            AddForeignKey("dbo.Reasons", "RootCauseID", "dbo.RootCauses", "ID", cascadeDelete: true);
        }
    }
}
