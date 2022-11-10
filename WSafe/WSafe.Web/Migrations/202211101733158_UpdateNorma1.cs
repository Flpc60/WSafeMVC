namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normas", "Item", c => c.String(nullable: false, maxLength: 6));
            DropColumn("dbo.Normas", "Codigo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Normas", "Codigo", c => c.String(nullable: false, maxLength: 6));
            DropColumn("dbo.Normas", "Item");
        }
    }
}
