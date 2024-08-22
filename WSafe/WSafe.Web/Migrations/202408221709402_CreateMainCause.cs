namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMainCause : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "MainCause1ID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "MainCause2ID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "MainCause3ID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "MainCause4ID", c => c.Int(nullable: false));
            AddColumn("dbo.Accions", "MainCause5ID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "MainCause1ID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "MainCause2ID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "MainCause3ID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "MainCause4ID", c => c.Int(nullable: false));
            AddColumn("dbo.AccionViewModels", "MainCause5ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccionViewModels", "MainCause5ID");
            DropColumn("dbo.AccionViewModels", "MainCause4ID");
            DropColumn("dbo.AccionViewModels", "MainCause3ID");
            DropColumn("dbo.AccionViewModels", "MainCause2ID");
            DropColumn("dbo.AccionViewModels", "MainCause1ID");
            DropColumn("dbo.Accions", "MainCause5ID");
            DropColumn("dbo.Accions", "MainCause4ID");
            DropColumn("dbo.Accions", "MainCause3ID");
            DropColumn("dbo.Accions", "MainCause2ID");
            DropColumn("dbo.Accions", "MainCause1ID");
        }
    }
}
