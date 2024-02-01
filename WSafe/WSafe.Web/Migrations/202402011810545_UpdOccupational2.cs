namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOccupational2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalRecomendationVMs", "Document", c => c.String());
            AddColumn("dbo.MedicalRecomendationVMs", "Age", c => c.String());
            AddColumn("dbo.MedicalRecomendationVMs", "Cargo", c => c.String());
            AlterColumn("dbo.MedicalRecomendationVMs", "Talla", c => c.String());
            AlterColumn("dbo.MedicalRecomendationVMs", "Peso", c => c.String());
            AlterColumn("dbo.Occupationals", "Talla", c => c.Short(nullable: false));
            AlterColumn("dbo.Occupationals", "Peso", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Occupationals", "Peso", c => c.Int(nullable: false));
            AlterColumn("dbo.Occupationals", "Talla", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalRecomendationVMs", "Peso", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalRecomendationVMs", "Talla", c => c.Int(nullable: false));
            DropColumn("dbo.MedicalRecomendationVMs", "Cargo");
            DropColumn("dbo.MedicalRecomendationVMs", "Age");
            DropColumn("dbo.MedicalRecomendationVMs", "Document");
        }
    }
}
