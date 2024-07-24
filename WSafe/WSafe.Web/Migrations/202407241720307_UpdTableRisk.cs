namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTableRisk : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Riesgoes", "FuenteControls");
            DropColumn("dbo.Riesgoes", "MedioControls");
            DropColumn("dbo.Riesgoes", "IndividuoControls");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 300));
            AddColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 300));
            AddColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 300));
        }
    }
}
