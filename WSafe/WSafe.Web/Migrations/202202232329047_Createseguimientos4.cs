namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createseguimientos4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors");
            DropIndex("dbo.Accions", new[] { "Trabajador_ID" });
            AddColumn("dbo.Accions", "TrabajadorID", c => c.Int(nullable: false));
            DropColumn("dbo.Accions", "Trabajador_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accions", "Trabajador_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Accions", "TrabajadorID");
            CreateIndex("dbo.Accions", "Trabajador_ID");
            AddForeignKey("dbo.Accions", "Trabajador_ID", "dbo.Trabajadors", "ID", cascadeDelete: true);
        }
    }
}
