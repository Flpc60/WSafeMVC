namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdEvaluationConcept : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvaluationConcepts", "VulnerabilityType", c => c.Int(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "EvaluationPerson", c => c.Int());
            AlterColumn("dbo.EvaluationConcepts", "EvaluationRecurso", c => c.Int());
            AlterColumn("dbo.EvaluationConcepts", "EvaluationSystem", c => c.Int());
            DropColumn("dbo.EvaluationConcepts", "VulnerabilitiType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvaluationConcepts", "VulnerabilitiType", c => c.Int(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "EvaluationSystem", c => c.Int(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "EvaluationRecurso", c => c.Int(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "EvaluationPerson", c => c.Int(nullable: false));
            AlterColumn("dbo.EvaluationConcepts", "Name", c => c.String());
            DropColumn("dbo.EvaluationConcepts", "VulnerabilityType");
        }
    }
}
