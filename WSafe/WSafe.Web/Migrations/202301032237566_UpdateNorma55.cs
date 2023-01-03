namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "Range", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "Range");
        }
    }
}
