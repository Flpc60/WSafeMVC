namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdMedicalRecomendation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalRecomendationVMs", "FechaIngreso", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalRecomendationVMs", "FechaIngreso");
        }
    }
}
