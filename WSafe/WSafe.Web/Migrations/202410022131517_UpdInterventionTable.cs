namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdInterventionTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Interventions", "Observaciones");
            DropColumn("dbo.Interventions", "NivelDeficiencia");
            DropColumn("dbo.Interventions", "NivelExposicion");
            DropColumn("dbo.Interventions", "NivelConsecuencia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interventions", "NivelConsecuencia", c => c.Short(nullable: false));
            AddColumn("dbo.Interventions", "NivelExposicion", c => c.Short(nullable: false));
            AddColumn("dbo.Interventions", "NivelDeficiencia", c => c.Short(nullable: false));
            AddColumn("dbo.Interventions", "Observaciones", c => c.String(nullable: false));
        }
    }
}
