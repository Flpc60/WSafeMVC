namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdplanActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActivityVMs", "Financieros", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "Administrativos", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "Tecnicos", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "Humanos", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivities", "Financieros", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivities", "Administrativos", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivities", "Tecnicos", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanActivities", "Humanos", c => c.Boolean(nullable: false));
            DropColumn("dbo.PlanActivityVMs", "Recurso");
            DropColumn("dbo.PlanActivityVMs", "TxtRecurso");
            DropColumn("dbo.PlanActivities", "Recurso");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanActivities", "Recurso", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "TxtRecurso", c => c.String());
            AddColumn("dbo.PlanActivityVMs", "Recurso", c => c.Int(nullable: false));
            DropColumn("dbo.PlanActivities", "Humanos");
            DropColumn("dbo.PlanActivities", "Tecnicos");
            DropColumn("dbo.PlanActivities", "Administrativos");
            DropColumn("dbo.PlanActivities", "Financieros");
            DropColumn("dbo.PlanActivityVMs", "Humanos");
            DropColumn("dbo.PlanActivityVMs", "Tecnicos");
            DropColumn("dbo.PlanActivityVMs", "Administrativos");
            DropColumn("dbo.PlanActivityVMs", "Financieros");
        }
    }
}
