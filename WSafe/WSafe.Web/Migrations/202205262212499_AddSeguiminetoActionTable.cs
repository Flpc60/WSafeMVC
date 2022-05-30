namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeguiminetoActionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeguimientoActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccionID = c.Int(nullable: false),
                        Resultado = c.String(nullable: false),
                        FechaSeguimiento = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
        }
    }
}
