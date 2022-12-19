namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normas", "Standard", c => c.String(nullable: false, maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normas", "Standard");
        }
    }
}
