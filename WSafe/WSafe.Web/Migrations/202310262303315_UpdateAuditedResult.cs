namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditedResult : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AuditedCreateVMs", "AuditChapter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditedCreateVMs", "AuditChapter", c => c.Int(nullable: false));
        }
    }
}
