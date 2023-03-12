namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimientVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientVMs", "Path", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientVMs", "Path");
        }
    }
}
