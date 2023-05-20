namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrabajadorVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkersVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrimerApellido = c.String(nullable: false, maxLength: 50),
                        SegundoApellido = c.String(maxLength: 50),
                        Nombres = c.String(nullable: false, maxLength: 50),
                        DocumentType = c.Int(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 20),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Genero = c.Int(nullable: false),
                        EstadoCivil = c.Int(nullable: false),
                        Direccion = c.String(nullable: false, maxLength: 50),
                        Telefonos = c.String(nullable: false, maxLength: 20),
                        FechaIngreso = c.DateTime(nullable: false),
                        TipoVinculacion = c.Int(nullable: false),
                        CargoID = c.Int(nullable: false),
                        EPS = c.String(nullable: false),
                        AFP = c.String(nullable: false, maxLength: 20),
                        ARL = c.String(nullable: false, maxLength: 20),
                        FechaRetiro = c.DateTime(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkersVMs");
        }
    }
}
