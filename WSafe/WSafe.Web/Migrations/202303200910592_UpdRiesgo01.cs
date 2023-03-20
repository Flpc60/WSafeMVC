namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdRiesgo01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 100));
            AddColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 100));
            AddColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 100));
            AddColumn("dbo.RiesgoViewModels", "FuenteControls", c => c.String(maxLength: 100));
            AddColumn("dbo.RiesgoViewModels", "MedioControls", c => c.String(maxLength: 100));
            AddColumn("dbo.RiesgoViewModels", "IndividuoControls", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "IndividuoControls");
            DropColumn("dbo.RiesgoViewModels", "MedioControls");
            DropColumn("dbo.RiesgoViewModels", "FuenteControls");
            DropColumn("dbo.Riesgoes", "IndividuoControls");
            DropColumn("dbo.Riesgoes", "MedioControls");
            DropColumn("dbo.Riesgoes", "FuenteControls");
        }
    }
}
