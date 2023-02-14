namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "StandardEvaluation", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "StandardMatrixRisk", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "StandardActions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "StandardActions");
            DropColumn("dbo.Organizations", "StandardMatrixRisk");
            DropColumn("dbo.Organizations", "StandardEvaluation");
        }
    }
}
