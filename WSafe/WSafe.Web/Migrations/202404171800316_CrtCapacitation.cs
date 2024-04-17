namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrtCapacitation : DbMigration
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
                        TrainingTopicID = c.String(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Programed = c.Short(nullable: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Capacitations", t => t.CapacitationID, cascadeDelete: true)
                .Index(t => t.CapacitationID);
            
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "CapacitationID", "dbo.Capacitations");
            DropIndex("dbo.Schedules", new[] { "CapacitationID" });
            DropTable("dbo.TrainingTopics");
            DropTable("dbo.Schedules");
            DropTable("dbo.Capacitations");
        }
    }
}
