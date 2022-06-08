namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDetailsVM : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PlanAccions", new[] { "_DetailsAccionVM_ID" });
            DropIndex("dbo.SeguimientoAccions", new[] { "_DetailsAccionVM_ID" });
            AddColumn("dbo.PlanActions", "_DetailsAccionVM_ID", c => c.Int());
            AddColumn("dbo.Seguimientoes", "_DetailsAccionVM_ID", c => c.Int());
            CreateIndex("dbo.PlanActions", "_DetailsAccionVM_ID");
            CreateIndex("dbo.Seguimientoes", "_DetailsAccionVM_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seguimientoes", new[] { "_DetailsAccionVM_ID" });
            DropIndex("dbo.PlanActions", new[] { "_DetailsAccionVM_ID" });
            DropColumn("dbo.Seguimientoes", "_DetailsAccionVM_ID");
            DropColumn("dbo.PlanActions", "_DetailsAccionVM_ID");
        }
    }
}
