namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalificationVM2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MinimalsStandardsVMs", "Color", c => c.String());
            AddColumn("dbo.MinimalsStandardsVMs", "TxtCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MinimalsStandardsVMs", "TxtCategory");
            DropColumn("dbo.MinimalsStandardsVMs", "Color");
        }
    }
}
