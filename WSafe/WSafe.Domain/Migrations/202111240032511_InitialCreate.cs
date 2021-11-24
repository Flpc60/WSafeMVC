namespace WSafe.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Peligroes", "CategoriaPeligro_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Consecuencias", "CategoriaPeligro_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Peligroes", "CategoriaPeligro_ID");
            CreateIndex("dbo.Consecuencias", "CategoriaPeligro_ID");
            AddForeignKey("dbo.Peligroes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Consecuencias", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes", "ID", cascadeDelete: true);
            DropColumn("dbo.Consecuencias", "CategoriaPeligroID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Consecuencias", "CategoriaPeligroID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Consecuencias", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes");
            DropForeignKey("dbo.Peligroes", "CategoriaPeligro_ID", "dbo.CategoriaPeligroes");
            DropIndex("dbo.Consecuencias", new[] { "CategoriaPeligro_ID" });
            DropIndex("dbo.Peligroes", new[] { "CategoriaPeligro_ID" });
            DropColumn("dbo.Consecuencias", "CategoriaPeligro_ID");
            DropColumn("dbo.Peligroes", "CategoriaPeligro_ID");
        }
    }
}
