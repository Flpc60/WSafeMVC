namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "ActionCategory", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "ActionCategory", c => c.Int(nullable: false));
            DropColumn("dbo.Accions", "Estado");
            DropColumn("dbo.AccionViewModels", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccionViewModels", "Estado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accions", "Estado", c => c.Boolean(nullable: false));
            DropColumn("dbo.AccionViewModels", "ActionCategory");
            DropColumn("dbo.Accions", "ActionCategory");
        }
    }
}
