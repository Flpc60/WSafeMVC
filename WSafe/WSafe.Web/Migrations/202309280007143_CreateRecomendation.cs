namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRecomendation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncidenteRecomendations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        RootCauseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SigueRecomendations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecomendationID = c.Int(nullable: false),
                        Seguimient = c.DateTime(nullable: false),
                        SupervisorConcept = c.String(maxLength: 200),
                        WorkerConcept = c.String(maxLength: 200),
                        SSTConcept = c.String(maxLength: 200),
                        Observations = c.String(maxLength: 200),
                        NewSeguimient = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Recomendations", t => t.RecomendationID, cascadeDelete: true)
                .Index(t => t.RecomendationID);
            
            AddColumn("dbo.Recomendations", "RecomendationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "Contingencia", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "TipoReintegro", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "CargoID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "CodigoDX", c => c.String(maxLength: 6));
            AddColumn("dbo.Recomendations", "EmisionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "Entity", c => c.String(maxLength: 20));
            AddColumn("dbo.Recomendations", "ReceptionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "PatologyID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "Duration", c => c.Short(nullable: false));
            AddColumn("dbo.Recomendations", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "FinalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "Compromise", c => c.Boolean(nullable: false));
            AddColumn("dbo.Recomendations", "Controls", c => c.String(maxLength: 200));
            AddColumn("dbo.Recomendations", "EPP", c => c.String(maxLength: 100));
            AddColumn("dbo.Recomendations", "Tasks", c => c.String(maxLength: 200));
            AddColumn("dbo.Recomendations", "WorkerCompromise", c => c.Boolean(nullable: false));
            AddColumn("dbo.Recomendations", "Observation", c => c.String(maxLength: 200));
            AddColumn("dbo.Recomendations", "CoordinadorID", c => c.Int(nullable: false));
            DropColumn("dbo.Recomendations", "IncidentID");
            DropColumn("dbo.Recomendations", "RootCauseID");
            DropColumn("dbo.Recomendations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recomendations", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Recomendations", "RootCauseID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "IncidentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SigueRecomendations", "RecomendationID", "dbo.Recomendations");
            DropIndex("dbo.SigueRecomendations", new[] { "RecomendationID" });
            DropColumn("dbo.Recomendations", "CoordinadorID");
            DropColumn("dbo.Recomendations", "Observation");
            DropColumn("dbo.Recomendations", "WorkerCompromise");
            DropColumn("dbo.Recomendations", "Tasks");
            DropColumn("dbo.Recomendations", "EPP");
            DropColumn("dbo.Recomendations", "Controls");
            DropColumn("dbo.Recomendations", "Compromise");
            DropColumn("dbo.Recomendations", "FinalDate");
            DropColumn("dbo.Recomendations", "InitialDate");
            DropColumn("dbo.Recomendations", "Duration");
            DropColumn("dbo.Recomendations", "PatologyID");
            DropColumn("dbo.Recomendations", "ReceptionDate");
            DropColumn("dbo.Recomendations", "Entity");
            DropColumn("dbo.Recomendations", "EmisionDate");
            DropColumn("dbo.Recomendations", "CodigoDX");
            DropColumn("dbo.Recomendations", "CargoID");
            DropColumn("dbo.Recomendations", "TipoReintegro");
            DropColumn("dbo.Recomendations", "Contingencia");
            DropColumn("dbo.Recomendations", "TrabajadorID");
            DropColumn("dbo.Recomendations", "RecomendationDate");
            DropTable("dbo.SigueRecomendations");
            DropTable("dbo.IncidenteRecomendations");
        }
    }
}
