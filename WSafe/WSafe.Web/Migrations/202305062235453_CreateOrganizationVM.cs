namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrganizationVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NIT = c.String(nullable: false, maxLength: 20),
                        RazonSocial = c.String(nullable: false, maxLength: 50),
                        Direccion = c.String(nullable: false, maxLength: 50),
                        Municip = c.String(nullable: false, maxLength: 50),
                        Department = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        ARL = c.String(nullable: false, maxLength: 20),
                        ClaseRiesgo = c.String(nullable: false, maxLength: 10),
                        DocumentType = c.Int(nullable: false),
                        DocumentRepresent = c.String(nullable: false, maxLength: 20),
                        NameRepresent = c.String(nullable: false, maxLength: 50),
                        EconomicActivity = c.String(nullable: false, maxLength: 100),
                        NumeroTrabajadores = c.Int(nullable: false),
                        Politica = c.String(nullable: false, maxLength: 150),
                        Products = c.String(nullable: false, maxLength: 150),
                        Mision = c.String(nullable: false, maxLength: 150),
                        Vision = c.String(nullable: false, maxLength: 150),
                        Objetivos = c.String(nullable: false, maxLength: 150),
                        Procesos = c.String(maxLength: 150),
                        TurnosAdministrativo = c.String(maxLength: 150),
                        TurnosOperativo = c.String(maxLength: 150),
                        Year = c.String(nullable: false, maxLength: 4),
                        Agropecuaria = c.Boolean(nullable: false),
                        ResponsableSGSST = c.String(maxLength: 50),
                        DocumentResponsable = c.String(maxLength: 20),
                        ResolucionLicencia = c.DateTime(nullable: false),
                        ResponsableLicencia = c.String(maxLength: 20),
                        RenovacionLicencia = c.DateTime(nullable: false),
                        RenovacionCurso = c.DateTime(nullable: false),
                        NivelEstudios = c.Int(nullable: false),
                        MesesExperiencia = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrganizationVMs");
        }
    }
}
