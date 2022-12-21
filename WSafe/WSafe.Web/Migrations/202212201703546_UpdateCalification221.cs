namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalification221 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Califications", "Justifica", c => c.Boolean(nullable: false));
            DropColumn("dbo.Califications", "NoCumple");
            DropColumn("dbo.Califications", "NoAplica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Califications", "NoAplica", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "NoCumple", c => c.Boolean(nullable: false));
            DropColumn("dbo.Califications", "Justifica");
        }
    }
}
