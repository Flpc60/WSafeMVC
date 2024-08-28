namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirmaElectronica", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FirmaElectronica");
        }
    }
}
