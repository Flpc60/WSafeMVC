namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditor2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditRiesgoes", "Operation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditRiesgoes", "Operation");
        }
    }
}
