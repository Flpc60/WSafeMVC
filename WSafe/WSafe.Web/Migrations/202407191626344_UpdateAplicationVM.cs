namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAplicationVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AplicacionVMs", "Finalidad", c => c.Int(nullable: false));
            AlterColumn("dbo.Aplicacions", "NivelDeficiencia", c => c.Short(nullable: false));
            AlterColumn("dbo.Aplicacions", "NivelExposicion", c => c.Short(nullable: false));
            AlterColumn("dbo.Aplicacions", "NivelConsecuencia", c => c.Short(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "Beneficios", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AplicacionVMs", "NivelDeficiencia", c => c.Short(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "NivelExposicion", c => c.Short(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "NivelConsecuencia", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AplicacionVMs", "NivelConsecuencia", c => c.Int(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "NivelExposicion", c => c.Int(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "NivelDeficiencia", c => c.Int(nullable: false));
            AlterColumn("dbo.AplicacionVMs", "Beneficios", c => c.String(maxLength: 100));
            AlterColumn("dbo.Aplicacions", "NivelConsecuencia", c => c.Int(nullable: false));
            AlterColumn("dbo.Aplicacions", "NivelExposicion", c => c.Int(nullable: false));
            AlterColumn("dbo.Aplicacions", "NivelDeficiencia", c => c.Int(nullable: false));
            DropColumn("dbo.AplicacionVMs", "Finalidad");
        }
    }
}
