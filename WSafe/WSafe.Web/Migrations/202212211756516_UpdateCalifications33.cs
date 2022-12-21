namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalifications33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Califications", "NoCumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "Justify", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "NoJustify", c => c.Boolean(nullable: false));
            DropColumn("dbo.Califications", "Justifica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Califications", "Justifica", c => c.Boolean(nullable: false));
            DropColumn("dbo.Califications", "NoJustify");
            DropColumn("dbo.Califications", "Justify");
            DropColumn("dbo.Califications", "NoCumple");
        }
    }
}
