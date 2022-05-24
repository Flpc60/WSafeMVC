namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccionVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccionViewModels", "Descripcion", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccionViewModels", "Descripcion", c => c.String(nullable: false));
        }
    }
}
