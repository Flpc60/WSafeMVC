namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActionVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionsMatrixVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaSolicitud = c.String(),
                        Zona = c.String(),
                        Proceso = c.String(),
                        Actividad = c.String(),
                        FuenteAccion = c.String(),
                        Categoria = c.String(),
                        Descripcion = c.String(maxLength: 100),
                        CategoriaCausa = c.String(),
                        Planes = c.String(),
                        FechaCierre = c.String(),
                        Responsable = c.String(),
                        ActionState = c.String(),
                        Efectiva = c.Boolean(nullable: false),
                        Eficacia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionsMatrixVMs");
        }
    }
}
