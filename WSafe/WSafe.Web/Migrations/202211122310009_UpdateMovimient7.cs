namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovimient7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovimientVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        Ciclo = c.String(nullable: false, maxLength: 2),
                        Item = c.String(nullable: false, maxLength: 6),
                        Name = c.String(nullable: false, maxLength: 200),
                        NormaID = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        Document = c.Binary(),
                        Year = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovimientVMs");
        }
    }
}
