namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDetailRecomendation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecomendationListVMs", "Description", c => c.String());
            AddColumn("dbo.RecomendationListVMs", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecomendationListVMs", "Type");
            DropColumn("dbo.RecomendationListVMs", "Description");
        }
    }
}
