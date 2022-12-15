namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEvaluation12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanActions", "EvaluationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "NormaID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "FechaActivity", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlanActions", "Observation", c => c.String(maxLength: 100));
            AddColumn("dbo.Evaluations", "FechaEvaluation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Evaluations", "Cumple", c => c.String(maxLength: 3));
            AddColumn("dbo.Evaluations", "NoCumple", c => c.String(maxLength: 3));
            AddColumn("dbo.Evaluations", "NoAplica", c => c.String(maxLength: 3));
            AddColumn("dbo.Evaluations", "StandarsResult", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Evaluations", "AplicationsResult", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Evaluations", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.Evaluations", "FechaEvaluacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Evaluations", "FechaEvaluacion", c => c.DateTime(nullable: false));
            DropColumn("dbo.Evaluations", "Category");
            DropColumn("dbo.Evaluations", "AplicationsResult");
            DropColumn("dbo.Evaluations", "StandarsResult");
            DropColumn("dbo.Evaluations", "NoAplica");
            DropColumn("dbo.Evaluations", "NoCumple");
            DropColumn("dbo.Evaluations", "Cumple");
            DropColumn("dbo.Evaluations", "FechaEvaluation");
            DropColumn("dbo.PlanActions", "Observation");
            DropColumn("dbo.PlanActions", "FechaActivity");
            DropColumn("dbo.PlanActions", "NormaID");
            DropColumn("dbo.PlanActions", "EvaluationID");
        }
    }
}
