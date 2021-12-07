namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaTablasRiesgos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RiesgoViewModels", name: "PeorConsecuencia_ID", newName: "ConsecuenciaID");
            RenameIndex(table: "dbo.RiesgoViewModels", name: "IX_PeorConsecuencia_ID", newName: "IX_ConsecuenciaID");
            AddColumn("dbo.RiesgoViewModels", "NivelesDeficiencia", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelesExposicion", c => c.Int(nullable: false));
            AddColumn("dbo.RiesgoViewModels", "NivelesConsecuencia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RiesgoViewModels", "NivelesConsecuencia");
            DropColumn("dbo.RiesgoViewModels", "NivelesExposicion");
            DropColumn("dbo.RiesgoViewModels", "NivelesDeficiencia");
            RenameIndex(table: "dbo.RiesgoViewModels", name: "IX_ConsecuenciaID", newName: "IX_PeorConsecuencia_ID");
            RenameColumn(table: "dbo.RiesgoViewModels", name: "ConsecuenciaID", newName: "PeorConsecuencia_ID");
        }
    }
}
