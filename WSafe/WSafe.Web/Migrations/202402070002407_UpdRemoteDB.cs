namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdRemoteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalRecomendationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExaminationDate = c.String(),
                        Trabajador = c.String(),
                        Talla = c.String(),
                        Peso = c.String(),
                        ExaminationType = c.String(),
                        Recomendations = c.String(),
                        MedicalRecomendation = c.String(),
                        Document = c.String(),
                        Age = c.String(),
                        Cargo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Occupationals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExaminationDate = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Recomendations = c.String(maxLength: 500),
                        Talla = c.Short(nullable: false),
                        Peso = c.Short(nullable: false),
                        ExaminationType = c.Int(nullable: false),
                        MedicalRecomendation = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SigueOccupationals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SigueDate = c.DateTime(nullable: false),
                        Resultado = c.String(nullable: false, maxLength: 200),
                        Recomendations = c.String(maxLength: 500),
                        OccupationalID = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Occupationals", t => t.OccupationalID, cascadeDelete: true)
                .Index(t => t.OccupationalID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SigueOccupationals", "OccupationalID", "dbo.Occupationals");
            DropIndex("dbo.SigueOccupationals", new[] { "OccupationalID" });
            DropTable("dbo.SigueOccupationals");
            DropTable("dbo.Occupationals");
            DropTable("dbo.MedicalRecomendationVMs");
        }
    }
}
