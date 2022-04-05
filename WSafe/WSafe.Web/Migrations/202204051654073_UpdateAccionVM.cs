namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccionVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccionViewModels", "FechaSolicitudStr", c => c.String());
            AddColumn("dbo.AccionViewModels", "FechaCierreStr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccionViewModels", "FechaCierreStr");
            DropColumn("dbo.AccionViewModels", "FechaSolicitudStr");
        }
    }
}
