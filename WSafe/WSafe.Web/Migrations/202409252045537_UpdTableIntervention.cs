namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTableIntervention : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrtVulnerabilityVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Types = c.Int(nullable: false),
                        CategoryAmenaza = c.Int(nullable: false),
                        AmenazaID = c.Int(nullable: false),
                        EvaluationConceptID = c.Int(nullable: false),
                        Response = c.Int(nullable: false),
                        Observation = c.String(maxLength: 500),
                        MyProperty = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Interventions", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interventions", "Description");
            DropTable("dbo.CrtVulnerabilityVMs");
        }
    }
}
