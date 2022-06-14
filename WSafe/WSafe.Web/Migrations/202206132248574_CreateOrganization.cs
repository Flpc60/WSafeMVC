namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrganization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NIT = c.String(nullable: false),
                        RazonSocial = c.String(nullable: false, maxLength: 50),
                        Direccion = c.String(nullable: false, maxLength: 50),
                        Municip = c.String(nullable: false, maxLength: 50),
                        Department = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        ARL = c.String(nullable: false, maxLength: 50),
                        ClaseRiesgo = c.String(nullable: false, maxLength: 10),
                        DocumentRepresent = c.String(nullable: false, maxLength: 20),
                        NameRepresent = c.String(nullable: false, maxLength: 50),
                        EconomicActivity = c.String(nullable: false, maxLength: 100),
                        NumeroTrabajadores = c.Int(nullable: false),
                        Products = c.String(nullable: false, maxLength: 50),
                        Mision = c.String(nullable: false, maxLength: 50),
                        Vision = c.String(nullable: false, maxLength: 50),
                        Objetivos = c.String(nullable: false, maxLength: 150),
                        Procesos = c.String(maxLength: 150),
                        Organigrama = c.String(maxLength: 150),
                        TurnosAdministrativo = c.String(maxLength: 150),
                        TurnosOperativo = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
        }
    }
}