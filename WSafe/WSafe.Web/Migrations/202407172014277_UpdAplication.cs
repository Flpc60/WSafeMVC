namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdAplication : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trazas", "ControlID", "dbo.Controls");
            DropIndex("dbo.Trazas", new[] { "ControlID" });
            CreateTable(
                "dbo.ControlTraces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ControlID = c.Int(nullable: false),
                        CtrlReplaceID = c.Int(nullable: false),
                        MaintCauseID = c.Int(nullable: false),
                        AplicacionID = c.Int(nullable: false),
                        RiesgoID = c.Int(nullable: false),
                        DateSigue = c.DateTime(nullable: false),
                        Efectividad = c.Boolean(nullable: false),
                        Observations = c.String(nullable: false, maxLength: 200),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Aplicacions", t => t.AplicacionID, cascadeDelete: true)
                .Index(t => t.AplicacionID);
            
            CreateTable(
                "dbo.MainCauses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Accions", "ControlTraceID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "ControlID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "Description", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Controls", "Eficacia", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Controls", "CategoriaEficacia", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Aplicacions", "Nombre", c => c.String(maxLength: 200));
            AlterColumn("dbo.Aplicacions", "Beneficios", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Controls", "Beneficios", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Controls", "AplicacionID");
            DropColumn("dbo.Controls", "RiesgoID");
            DropColumn("dbo.Controls", "Codigo");
            DropColumn("dbo.Controls", "Nombre");
            DropColumn("dbo.Controls", "Efectividad");
            DropColumn("dbo.Controls", "CategoriaEfectividad");
            DropTable("dbo.Trazas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Trazas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ControlID = c.Int(nullable: false),
                        AplicacionID = c.Int(nullable: false),
                        RiesgoolID = c.Int(nullable: false),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Efectividad = c.Int(nullable: false),
                        Presupuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstadoActual = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Controls", "CategoriaEfectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "Efectividad", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.Controls", "Codigo", c => c.String(nullable: false));
            AddColumn("dbo.Controls", "RiesgoID", c => c.Int(nullable: false));
            AddColumn("dbo.Controls", "AplicacionID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ControlTraces", "AplicacionID", "dbo.Aplicacions");
            DropIndex("dbo.ControlTraces", new[] { "AplicacionID" });
            AlterColumn("dbo.Controls", "Beneficios", c => c.String());
            AlterColumn("dbo.Aplicacions", "Beneficios", c => c.String());
            AlterColumn("dbo.Aplicacions", "Nombre", c => c.String());
            DropColumn("dbo.Controls", "UserID");
            DropColumn("dbo.Controls", "ClientID");
            DropColumn("dbo.Controls", "OrganizationID");
            DropColumn("dbo.Controls", "CategoriaEficacia");
            DropColumn("dbo.Controls", "Eficacia");
            DropColumn("dbo.Controls", "Description");
            DropColumn("dbo.Aplicacions", "ControlID");
            DropColumn("dbo.Accions", "ControlTraceID");
            DropTable("dbo.MainCauses");
            DropTable("dbo.ControlTraces");
            CreateIndex("dbo.Trazas", "ControlID");
            AddForeignKey("dbo.Trazas", "ControlID", "dbo.Controls", "ID", cascadeDelete: true);
        }
    }
}
