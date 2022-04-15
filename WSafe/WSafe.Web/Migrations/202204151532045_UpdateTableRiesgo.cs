namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableRiesgo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "PeorConsecuencia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Riesgoes", "PeorConsecuencia");
        }
    }
}
