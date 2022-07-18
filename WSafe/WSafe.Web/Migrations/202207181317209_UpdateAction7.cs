namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAction7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccionViewModels", "FechaCierre", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccionViewModels", "FechaCierre", c => c.DateTime(nullable: false));
        }
    }
}
