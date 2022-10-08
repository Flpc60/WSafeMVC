namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarrierAnalice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        BarrierCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CausalAnalice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        CausalFactor = c.Int(nullable: false),
                        PotencialFactor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventsOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reasons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Recomendations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        RootCauseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RootCauses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RootCauses");
            DropTable("dbo.Recomendations");
            DropTable("dbo.Reasons");
            DropTable("dbo.EventsOrders");
            DropTable("dbo.Events");
            DropTable("dbo.CausalAnalice");
            DropTable("dbo.BarrierAnalice");
        }
    }
}
