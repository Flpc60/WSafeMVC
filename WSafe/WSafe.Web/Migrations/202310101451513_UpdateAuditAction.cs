namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditAction : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AuditActions", newName: "AuditedActions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AuditedActions", newName: "AuditActions");
        }
    }
}
