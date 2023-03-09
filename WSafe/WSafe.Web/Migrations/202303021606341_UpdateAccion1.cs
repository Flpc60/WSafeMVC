namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccion1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PlanActions", "AccionID");
            AddForeignKey("dbo.PlanActions", "AccionID", "dbo.Accions", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanActions", "AccionID", "dbo.Accions");
            DropIndex("dbo.PlanActions", new[] { "AccionID" });
        }
    }
}
