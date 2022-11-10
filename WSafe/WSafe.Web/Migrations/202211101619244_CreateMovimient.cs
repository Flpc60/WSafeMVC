namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMovimient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Califications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EvaluationID = c.Int(nullable: false),
                        NormaID = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        FechaEvaluacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movimients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        NormaID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Document = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Normas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        Ciclo = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 200),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Normas");
            DropTable("dbo.Movimients");
            DropTable("dbo.Evaluations");
            DropTable("dbo.Califications");
        }
    }
}
