namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRiesgoVM3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MatrizRiesgosVMs", "Rutinaria", c => c.String());
            AlterColumn("dbo.MatrizRiesgosVMs", "RequisitoLegal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatrizRiesgosVMs", "RequisitoLegal", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MatrizRiesgosVMs", "Rutinaria", c => c.Boolean(nullable: false));
        }
    }
}
