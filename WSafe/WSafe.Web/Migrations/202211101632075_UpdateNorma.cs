namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normas", "Codigo", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normas", "Codigo");
        }
    }
}
