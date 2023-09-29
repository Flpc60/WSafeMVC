namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRecomendation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SigueRecomendations", "RecomendationListVM_ID", "dbo.RecomendationListVMs");
            DropIndex("dbo.SigueRecomendations", new[] { "RecomendationListVM_ID" });
            AddColumn("dbo.Recomendations", "PatologyID", c => c.Int(nullable: false));
            AlterColumn("dbo.RecomendationListVMs", "Compromise", c => c.String(nullable: false));
            AlterColumn("dbo.RecomendationListVMs", "WorkerCompromise", c => c.String(nullable: false));
            DropColumn("dbo.SigueRecomendations", "RecomendationListVM_ID");
            DropColumn("dbo.Recomendations", "PatolgyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recomendations", "PatolgyID", c => c.Int(nullable: false));
            AddColumn("dbo.SigueRecomendations", "RecomendationListVM_ID", c => c.Int());
            AlterColumn("dbo.RecomendationListVMs", "WorkerCompromise", c => c.Boolean(nullable: false));
            AlterColumn("dbo.RecomendationListVMs", "Compromise", c => c.Boolean(nullable: false));
            DropColumn("dbo.Recomendations", "PatologyID");
            CreateIndex("dbo.SigueRecomendations", "RecomendationListVM_ID");
            AddForeignKey("dbo.SigueRecomendations", "RecomendationListVM_ID", "dbo.RecomendationListVMs", "ID");
        }
    }
}
