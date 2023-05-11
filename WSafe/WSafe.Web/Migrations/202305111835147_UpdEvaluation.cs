namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdEvaluation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evaluations", "ClientID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evaluations", "ClientID");
        }
    }
}
