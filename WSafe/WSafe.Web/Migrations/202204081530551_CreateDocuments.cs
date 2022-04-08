namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Formato = c.String(),
                        Estandar = c.String(),
                        Titulo = c.String(),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.LoginViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Documents");
        }
    }
}
