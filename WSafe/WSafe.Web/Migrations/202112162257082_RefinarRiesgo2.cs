namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefinarRiesgo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Riesgoes", "NivelProbabilidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Riesgoes", "NivelProbabilidad");
        }
    }
}
