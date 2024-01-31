namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOccupational1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicalRecomendationVMs", "ExaminationDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicalRecomendationVMs", "ExaminationDate", c => c.DateTime(nullable: false));
        }
    }
}
