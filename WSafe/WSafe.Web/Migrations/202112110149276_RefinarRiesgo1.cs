namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias");
            DropIndex("dbo.Riesgoes", new[] { "PeorConsecuencia_ID" });
            DropColumn("dbo.Riesgoes", "PeorConsecuencia_ID");
            DropTable("dbo.Consecuencias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Consecuencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NivelConsecuencia = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Riesgoes", "PeorConsecuencia_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Riesgoes", "PeorConsecuencia_ID");
            AddForeignKey("dbo.Riesgoes", "PeorConsecuencia_ID", "dbo.Consecuencias", "ID", cascadeDelete: true);
        }
    }
}
