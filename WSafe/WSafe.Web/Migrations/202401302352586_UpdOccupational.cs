namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOccupational : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SigueOccupationals", "MedicalRecomendationVM_ID", "dbo.MedicalRecomendationVMs");
            DropIndex("dbo.SigueOccupationals", new[] { "MedicalRecomendationVM_ID" });
            DropColumn("dbo.SigueOccupationals", "MedicalRecomendationVM_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SigueOccupationals", "MedicalRecomendationVM_ID", c => c.Int());
            CreateIndex("dbo.SigueOccupationals", "MedicalRecomendationVM_ID");
            AddForeignKey("dbo.SigueOccupationals", "MedicalRecomendationVM_ID", "dbo.MedicalRecomendationVMs", "ID");
        }
    }
}
