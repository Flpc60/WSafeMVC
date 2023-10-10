namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auditers", "AuditListVM_ID", "dbo.AuditListVMs");
            DropForeignKey("dbo.Auditers", "AuditID", "dbo.Audits");
            DropIndex("dbo.Auditers", new[] { "AuditID" });
            DropIndex("dbo.Auditers", new[] { "AuditListVM_ID" });
            AddColumn("dbo.AuditListVMs", "Auditer", c => c.String(nullable: false));
            AddColumn("dbo.Audits", "AuditerID", c => c.Int(nullable: false));
            DropColumn("dbo.Auditers", "AuditID");
            DropColumn("dbo.Auditers", "AuditListVM_ID");
            DropColumn("dbo.AuditListVMs", "TxtAuditers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditListVMs", "TxtAuditers", c => c.String(nullable: false));
            AddColumn("dbo.Auditers", "AuditListVM_ID", c => c.Int());
            AddColumn("dbo.Auditers", "AuditID", c => c.Int(nullable: false));
            DropColumn("dbo.Audits", "AuditerID");
            DropColumn("dbo.AuditListVMs", "Auditer");
            CreateIndex("dbo.Auditers", "AuditListVM_ID");
            CreateIndex("dbo.Auditers", "AuditID");
            AddForeignKey("dbo.Auditers", "AuditID", "dbo.Audits", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Auditers", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
        }
    }
}
