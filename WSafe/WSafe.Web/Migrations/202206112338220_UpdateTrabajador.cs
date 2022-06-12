namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrabajador : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes");
            DropIndex("dbo.Trabajadors", new[] { "Cargo_ID" });
            AddColumn("dbo.Trabajadors", "CargoID", c => c.Int(nullable: false));
            DropColumn("dbo.Trabajadors", "Cargo_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajadors", "Cargo_ID", c => c.Int());
            DropColumn("dbo.Trabajadors", "CargoID");
            CreateIndex("dbo.Trabajadors", "Cargo_ID");
            AddForeignKey("dbo.Trabajadors", "Cargo_ID", "dbo.Cargoes", "ID");
        }
    }
}
