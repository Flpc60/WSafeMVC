namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAplicaVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AplicacionVMs", "FechaInicial", c => c.String(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "FechaFinal", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AplicacionVMs", "FechaFinal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "FechaInicial", c => c.DateTime(nullable: false));
        }
    }
}
