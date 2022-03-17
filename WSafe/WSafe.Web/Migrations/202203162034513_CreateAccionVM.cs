namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccionVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccionViewModels", "Origen", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccionViewModels", "Origen");
        }
    }
}
