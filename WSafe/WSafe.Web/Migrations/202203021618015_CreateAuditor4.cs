namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditor4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditaAccions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Usuario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditaAccions");
        }
    }
}
