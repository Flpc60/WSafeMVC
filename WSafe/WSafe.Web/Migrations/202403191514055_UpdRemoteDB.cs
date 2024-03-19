namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdRemoteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateOccupationalVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExaminationDate = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Talla = c.Short(nullable: false),
                        Peso = c.Short(nullable: false),
                        ExaminationType = c.Int(nullable: false),
                        Recomendations = c.String(maxLength: 500),
                        MedicalRecomendation = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.MedicalRecomendationVMs", "FechaIngreso", c => c.String());
            AddColumn("dbo.Occupationals", "FileName", c => c.String(maxLength: 200));
            AddColumn("dbo.SigueOccupationals", "CreateOccupationalVM_ID", c => c.Int());
            AddColumn("dbo.Organizations", "StandardOccupational", c => c.Short(nullable: false));
            CreateIndex("dbo.SigueOccupationals", "CreateOccupationalVM_ID");
            AddForeignKey("dbo.SigueOccupationals", "CreateOccupationalVM_ID", "dbo.CreateOccupationalVMs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SigueOccupationals", "CreateOccupationalVM_ID", "dbo.CreateOccupationalVMs");
            DropIndex("dbo.SigueOccupationals", new[] { "CreateOccupationalVM_ID" });
            DropColumn("dbo.Organizations", "StandardOccupational");
            DropColumn("dbo.SigueOccupationals", "CreateOccupationalVM_ID");
            DropColumn("dbo.Occupationals", "FileName");
            DropColumn("dbo.MedicalRecomendationVMs", "FechaIngreso");
            DropTable("dbo.CreateOccupationalVMs");
        }
    }
}
