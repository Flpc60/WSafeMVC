namespace WSafe.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePeligros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Peligroes", "CategoriaPeligroID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Peligroes", "CategoriaPeligroID");
        }
    }
}
