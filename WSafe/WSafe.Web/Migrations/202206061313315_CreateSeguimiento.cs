namespace WSafe.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateSeguimiento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seguimientoes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    AccionID = c.Int(nullable: false),
                    Resultado = c.String(nullable: false),
                    FechaSeguimiento = c.DateTime(nullable: false),
                    TrabajadorID = c.Int(nullable: false),
                    Responsable = c.String(),
                    AccionViewModel_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .Index(t => t.AccionViewModel_ID);
        }

        public override void Down()
        {
        }
    }
}
