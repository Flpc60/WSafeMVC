namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCapacitationTable : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Capacitations", "Executed", c => c.Short(nullable: false));
            AddColumn("dbo.Schedules", "CreateCapacitationVM_ID", c => c.Int());
            AddColumn("dbo.TrainingTopics", "OrganizationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "CreateCapacitationVM_ID");
            AddForeignKey("dbo.Schedules", "CreateCapacitationVM_ID", "dbo.CreateCapacitationVMs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "CreateCapacitationVM_ID", "dbo.CreateCapacitationVMs");
            DropIndex("dbo.Schedules", new[] { "CreateCapacitationVM_ID" });
            DropColumn("dbo.TrainingTopics", "OrganizationID");
            DropColumn("dbo.Schedules", "CreateCapacitationVM_ID");
            DropColumn("dbo.Capacitations", "Executed");
            DropTable("dbo.CreateCapacitationVMs");
        }
    }
}
