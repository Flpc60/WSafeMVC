namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDB2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuditedActions", "AuditListVM_ID", "dbo.AuditListVMs");
            DropForeignKey("dbo.AuditedResults", "AuditListVM_ID", "dbo.AuditListVMs");
            DropForeignKey("dbo.SigueAudits", "AuditListVM_ID", "dbo.AuditListVMs");
            DropIndex("dbo.AuditedActions", new[] { "AuditListVM_ID" });
            DropIndex("dbo.AuditedResults", new[] { "AuditListVM_ID" });
            DropIndex("dbo.SigueAudits", new[] { "AuditListVM_ID" });
            //DropColumn("dbo.AuditedActions", "AuditListVM_ID");
            //DropColumn("dbo.AuditedResults", "AuditListVM_ID");
            DropColumn("dbo.SigueAudits", "AuditListVM_ID");
            DropTable("dbo.AuditListVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuditListVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditDate = c.String(nullable: false),
                        Process = c.String(nullable: false),
                        Responsable = c.String(nullable: false),
                        Auditer = c.String(nullable: false),
                        NoConformance = c.String(nullable: false, maxLength: 100),
                        Cause = c.String(nullable: false, maxLength: 100),
                        CorrectiveAction = c.String(nullable: false, maxLength: 100),
                        ResponsableAction = c.String(nullable: false),
                        ExecutionDate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.SigueAudits", "AuditListVM_ID", c => c.Int());
            AddColumn("dbo.AuditedResults", "AuditListVM_ID", c => c.Int());
            AddColumn("dbo.AuditedActions", "AuditListVM_ID", c => c.Int());
            CreateIndex("dbo.SigueAudits", "AuditListVM_ID");
            CreateIndex("dbo.AuditedResults", "AuditListVM_ID");
            CreateIndex("dbo.AuditedActions", "AuditListVM_ID");
            AddForeignKey("dbo.SigueAudits", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
            AddForeignKey("dbo.AuditedResults", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
            AddForeignKey("dbo.AuditedActions", "AuditListVM_ID", "dbo.AuditListVMs", "ID");
        }
    }
}
