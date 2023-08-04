namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgos11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "FuenteControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "MedioControls", c => c.String(maxLength: 300));
            AlterColumn("dbo.RiesgoViewModels", "IndividuoControls", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RiesgoViewModels", "IndividuoControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.RiesgoViewModels", "MedioControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.RiesgoViewModels", "FuenteControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "IndividuoControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "MedioControls", c => c.String(maxLength: 100));
            AlterColumn("dbo.Riesgoes", "FuenteControls", c => c.String(maxLength: 100));
        }
    }
}
