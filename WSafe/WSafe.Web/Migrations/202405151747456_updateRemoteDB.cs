namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRemoteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Capacitations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InitialDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TrainingTopicID = c.Int(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Programed = c.Short(nullable: false),
                        Executed = c.Short(nullable: false),
                        Citados = c.Short(nullable: false),
                        Capacitados = c.Short(nullable: false),
                        Evaluados = c.Short(nullable: false),
                        ActivityFrequency = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateSigue = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Executed = c.Short(nullable: false),
                        Programed = c.Short(nullable: false),
                        Citados = c.Short(nullable: false),
                        Capacitados = c.Short(nullable: false),
                        Evaluados = c.Short(nullable: false),
                        CapacitationID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CreateCapacitationVM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Capacitations", t => t.CapacitationID, cascadeDelete: true)
                .ForeignKey("dbo.CreateCapacitationVMs", t => t.CreateCapacitationVM_ID)
                .Index(t => t.CapacitationID)
                .Index(t => t.CreateCapacitationVM_ID);
            
            CreateTable(
                "dbo.CreateCapacitationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrainingTopicID = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                        Objetive = c.String(maxLength: 200),
                        Content = c.String(maxLength: 200),
                        Resources = c.String(maxLength: 200),
                        TrabajadorID = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Programed = c.Short(nullable: false),
                        Executed = c.Short(nullable: false),
                        Citados = c.Short(nullable: false),
                        Capacitados = c.Short(nullable: false),
                        Evaluados = c.Short(nullable: false),
                        InitialDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActivityFrequency = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ListCapacitationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Objetive = c.String(maxLength: 200),
                        Content = c.String(maxLength: 100),
                        Resources = c.String(maxLength: 100),
                        Responsable = c.String(maxLength: 100),
                        Eficacia = c.String(),
                        Eficiencia = c.String(),
                        Efectividad = c.String(),
                        P1 = c.Short(nullable: false),
                        E1 = c.Short(nullable: false),
                        Cc1 = c.Short(nullable: false),
                        C1 = c.Short(nullable: false),
                        Tc1 = c.Short(nullable: false),
                        P2 = c.Short(nullable: false),
                        E2 = c.Short(nullable: false),
                        Cc2 = c.Short(nullable: false),
                        C2 = c.Short(nullable: false),
                        Tc2 = c.Short(nullable: false),
                        P3 = c.Short(nullable: false),
                        E3 = c.Short(nullable: false),
                        Cc3 = c.Short(nullable: false),
                        C3 = c.Short(nullable: false),
                        Tc3 = c.Short(nullable: false),
                        P4 = c.Short(nullable: false),
                        E4 = c.Short(nullable: false),
                        Cc4 = c.Short(nullable: false),
                        C4 = c.Short(nullable: false),
                        Tc4 = c.Short(nullable: false),
                        P5 = c.Short(nullable: false),
                        E5 = c.Short(nullable: false),
                        Cc5 = c.Short(nullable: false),
                        C5 = c.Short(nullable: false),
                        Tc5 = c.Short(nullable: false),
                        P6 = c.Short(nullable: false),
                        E6 = c.Short(nullable: false),
                        Cc6 = c.Short(nullable: false),
                        C6 = c.Short(nullable: false),
                        Tc6 = c.Short(nullable: false),
                        P7 = c.Short(nullable: false),
                        E7 = c.Short(nullable: false),
                        Cc7 = c.Short(nullable: false),
                        C7 = c.Short(nullable: false),
                        Tc7 = c.Short(nullable: false),
                        P8 = c.Short(nullable: false),
                        E8 = c.Short(nullable: false),
                        Cc8 = c.Short(nullable: false),
                        C8 = c.Short(nullable: false),
                        Tc8 = c.Short(nullable: false),
                        P9 = c.Short(nullable: false),
                        E9 = c.Short(nullable: false),
                        Cc9 = c.Short(nullable: false),
                        C9 = c.Short(nullable: false),
                        Tc9 = c.Short(nullable: false),
                        P10 = c.Short(nullable: false),
                        E10 = c.Short(nullable: false),
                        Cc10 = c.Short(nullable: false),
                        C10 = c.Short(nullable: false),
                        Tc10 = c.Short(nullable: false),
                        P11 = c.Short(nullable: false),
                        E11 = c.Short(nullable: false),
                        Cc11 = c.Short(nullable: false),
                        C11 = c.Short(nullable: false),
                        Tc11 = c.Short(nullable: false),
                        P12 = c.Short(nullable: false),
                        E12 = c.Short(nullable: false),
                        Cc12 = c.Short(nullable: false),
                        C12 = c.Short(nullable: false),
                        Tc12 = c.Short(nullable: false),
                        P13 = c.Short(nullable: false),
                        E13 = c.Short(nullable: false),
                        Cc13 = c.Short(nullable: false),
                        C13 = c.Short(nullable: false),
                        Tc13 = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrainingTopics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Objetive = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false, maxLength: 200),
                        Resources = c.String(nullable: false, maxLength: 200),
                        ActivityFrequency = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Organizations", "StandardCapacitation", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "CreateCapacitationVM_ID", "dbo.CreateCapacitationVMs");
            DropForeignKey("dbo.Schedules", "CapacitationID", "dbo.Capacitations");
            DropIndex("dbo.Schedules", new[] { "CreateCapacitationVM_ID" });
            DropIndex("dbo.Schedules", new[] { "CapacitationID" });
            DropColumn("dbo.Organizations", "StandardCapacitation");
            DropTable("dbo.TrainingTopics");
            DropTable("dbo.ListCapacitationVMs");
            DropTable("dbo.CreateCapacitationVMs");
            DropTable("dbo.Schedules");
            DropTable("dbo.Capacitations");
        }
    }
}
