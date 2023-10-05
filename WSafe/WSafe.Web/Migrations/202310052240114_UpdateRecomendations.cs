namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRecomendations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patologies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 6),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecomendationListVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Trabajador = c.String(nullable: false),
                        Contingencia = c.String(),
                        TipoReintegro = c.String(),
                        Cargo = c.String(nullable: false),
                        Patology = c.String(),
                        EmisionDate = c.DateTime(nullable: false),
                        Emision = c.String(nullable: false),
                        Entity = c.String(maxLength: 20),
                        ReceptionDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Type = c.String(),
                        InitialDate = c.DateTime(nullable: false),
                        FinalDate = c.DateTime(nullable: false),
                        Duration = c.Short(nullable: false),
                        Compromise = c.String(nullable: false),
                        Controls = c.String(nullable: false, maxLength: 200),
                        EPP = c.String(nullable: false, maxLength: 100),
                        Tasks = c.String(nullable: false, maxLength: 200),
                        WorkerCompromise = c.String(nullable: false),
                        Observation = c.String(nullable: false, maxLength: 200),
                        Coordinador = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SigueRecomendations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecomendationID = c.Int(nullable: false),
                        SeguimientDate = c.DateTime(nullable: false),
                        SupervisorReport = c.String(nullable: false, maxLength: 200),
                        WorkerReport = c.String(nullable: false, maxLength: 200),
                        SSTReport = c.String(nullable: false, maxLength: 200),
                        Observations = c.String(nullable: false, maxLength: 200),
                        NewSeguimient = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Recomendations", t => t.RecomendationID, cascadeDelete: true)
                .Index(t => t.RecomendationID);
            
            CreateTable(
                "dbo.RecomendationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkerID = c.Int(nullable: false),
                        Contingencia = c.Int(nullable: false),
                        TipoReintegro = c.Int(nullable: false),
                        CargoID = c.Int(nullable: false),
                        PatologyID = c.Int(nullable: false),
                        EmisionDate = c.DateTime(nullable: false),
                        Emision = c.Int(nullable: false),
                        Entity = c.String(nullable: false, maxLength: 20),
                        ReceptionDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        Type = c.Int(nullable: false),
                        Duration = c.Short(nullable: false),
                        InitialDate = c.DateTime(nullable: false),
                        FinalDate = c.DateTime(nullable: false),
                        Compromise = c.Boolean(nullable: false),
                        Controls = c.String(nullable: false, maxLength: 200),
                        Investigation = c.String(nullable: false, maxLength: 200),
                        EPP = c.String(nullable: false, maxLength: 100),
                        Tasks = c.String(nullable: false, maxLength: 200),
                        WorkerCompromise = c.Boolean(nullable: false),
                        Observation = c.String(nullable: false, maxLength: 200),
                        CoordinadorID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        RootCauseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Organizations", "StandardRecomendations", c => c.Short(nullable: false));
            AddColumn("dbo.Recomendations", "RecomendationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "TrabajadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "Contingencia", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "TipoReintegro", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "CargoID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "PatologyID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "EmisionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "Emision", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "Entity", c => c.String());
            AddColumn("dbo.Recomendations", "ReceptionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "Description", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Recomendations", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "Duration", c => c.Short(nullable: false));
            AddColumn("dbo.Recomendations", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "FinalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recomendations", "Compromise", c => c.Boolean(nullable: false));
            AddColumn("dbo.Recomendations", "Controls", c => c.String());
            AddColumn("dbo.Recomendations", "Investigation", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Recomendations", "EPP", c => c.String());
            AddColumn("dbo.Recomendations", "Tasks", c => c.String());
            AddColumn("dbo.Recomendations", "WorkerCompromise", c => c.Boolean(nullable: false));
            AddColumn("dbo.Recomendations", "Observation", c => c.String());
            AddColumn("dbo.Recomendations", "CoordinadorID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Recomendations", "UserID", c => c.Int(nullable: false));
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
            DropColumn("dbo.Recomendations", "UserID");
            DropColumn("dbo.Recomendations", "ClientID");
            DropColumn("dbo.Recomendations", "OrganizationID");
            DropColumn("dbo.Recomendations", "CoordinadorID");
            DropColumn("dbo.Recomendations", "Observation");
            DropColumn("dbo.Recomendations", "WorkerCompromise");
            DropColumn("dbo.Recomendations", "Tasks");
            DropColumn("dbo.Recomendations", "EPP");
            DropColumn("dbo.Recomendations", "Investigation");
            DropColumn("dbo.Recomendations", "Controls");
            DropColumn("dbo.Recomendations", "Compromise");
            DropColumn("dbo.Recomendations", "FinalDate");
            DropColumn("dbo.Recomendations", "InitialDate");
            DropColumn("dbo.Recomendations", "Duration");
            DropColumn("dbo.Recomendations", "Type");
            DropColumn("dbo.Recomendations", "Description");
            DropColumn("dbo.Recomendations", "ReceptionDate");
            DropColumn("dbo.Recomendations", "Entity");
            DropColumn("dbo.Recomendations", "Emision");
            DropColumn("dbo.Recomendations", "EmisionDate");
            DropColumn("dbo.Recomendations", "PatologyID");
            DropColumn("dbo.Recomendations", "CargoID");
            DropColumn("dbo.Recomendations", "TipoReintegro");
            DropColumn("dbo.Recomendations", "Contingencia");
            DropColumn("dbo.Recomendations", "TrabajadorID");
            DropColumn("dbo.Recomendations", "RecomendationDate");
            DropColumn("dbo.Organizations", "StandardRecomendations");
            DropTable("dbo.Suggestions");
            DropTable("dbo.RecomendationVMs");
            DropTable("dbo.SigueRecomendations");
            DropTable("dbo.RecomendationListVMs");
            DropTable("dbo.Patologies");
        }
    }
}
