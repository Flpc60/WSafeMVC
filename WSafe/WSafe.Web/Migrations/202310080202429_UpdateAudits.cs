namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAudits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditListVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditDate = c.String(nullable: false),
                        Process = c.String(nullable: false),
                        Responsable = c.String(nullable: false),
                        Auditers = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SigueAudits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditID = c.Int(nullable: false),
                        SeguimientDate = c.DateTime(nullable: false),
                        SupervisorReport = c.String(nullable: false, maxLength: 200),
                        WorkerReport = c.String(nullable: false, maxLength: 200),
                        SSTReport = c.String(nullable: false, maxLength: 200),
                        Observations = c.String(nullable: false, maxLength: 200),
                        AuditListVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AuditListVMs", t => t.AuditListVM_ID)
                .ForeignKey("dbo.Audits", t => t.AuditID, cascadeDelete: true)
                .Index(t => t.AuditID)
                .Index(t => t.AuditListVM_ID);
            
            AddColumn("dbo.AuditActions", "AuditListVM_ID", c => c.Int());
            AddColumn("dbo.AuditedResults", "AuditListVM_ID", c => c.Int());
            AddColumn("dbo.Auditers", "AuditID", c => c.Int(nullable: false));
            AddColumn("dbo.Audits", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Audits", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Audits", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.AuditActions", "AuditListVM_ID");
            CreateIndex("dbo.AuditedResults", "AuditListVM_ID");
            CreateIndex("dbo.Auditers", "AuditID");
            AddForeignKey("dbo.AuditActions", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
            AddForeignKey("dbo.AuditedResults", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
            AddForeignKey("dbo.Auditers", "AuditID", "dbo.Audits", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SigueAudits", "AuditID", "dbo.Audits");
            DropForeignKey("dbo.Auditers", "AuditID", "dbo.Audits");
            DropForeignKey("dbo.SigueAudits", "AuditListVM_ID", "dbo.AuditListVMs");
            DropForeignKey("dbo.AuditedResults", "AuditListVM_ID", "dbo.AuditListVMs");
            DropForeignKey("dbo.AuditActions", "AuditListVM_ID", "dbo.AuditListVMs");
            DropIndex("dbo.SigueAudits", new[] { "AuditListVM_ID" });
            DropIndex("dbo.SigueAudits", new[] { "AuditID" });
            DropIndex("dbo.Auditers", new[] { "AuditID" });
            DropIndex("dbo.AuditedResults", new[] { "AuditListVM_ID" });
            DropIndex("dbo.AuditActions", new[] { "AuditListVM_ID" });
            DropColumn("dbo.Audits", "UserID");
            DropColumn("dbo.Audits", "ClientID");
            DropColumn("dbo.Audits", "OrganizationID");
            DropColumn("dbo.Auditers", "AuditID");
            DropColumn("dbo.AuditedResults", "AuditListVM_ID");
            DropColumn("dbo.AuditActions", "AuditListVM_ID");
            DropTable("dbo.SigueAudits");
            DropTable("dbo.AuditListVMs");
        }
    }
}
