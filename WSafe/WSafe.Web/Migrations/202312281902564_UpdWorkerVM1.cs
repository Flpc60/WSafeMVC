namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorkerVM1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trabajadors", "DiasPago", c => c.Short(nullable: false));
            AlterColumn("dbo.Trabajadors", "Conyuge", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "NumberHijos", c => c.Short(nullable: false));
            AlterColumn("dbo.Trabajadors", "Enfermedad", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "Tratamiento", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "SpecialRecomendations", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Conyuge", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "NumberHijos", c => c.Short(nullable: false));
            AlterColumn("dbo.WorkersVMs", "Enfermedad", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Tratamiento", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "SpecialRecomendations", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkersVMs", "SpecialRecomendations", c => c.String());
            AlterColumn("dbo.WorkersVMs", "Tratamiento", c => c.String());
            AlterColumn("dbo.WorkersVMs", "Enfermedad", c => c.String());
            AlterColumn("dbo.WorkersVMs", "NumberHijos", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkersVMs", "Conyuge", c => c.String());
            AlterColumn("dbo.Trabajadors", "SpecialRecomendations", c => c.String());
            AlterColumn("dbo.Trabajadors", "Tratamiento", c => c.String());
            AlterColumn("dbo.Trabajadors", "Enfermedad", c => c.String());
            AlterColumn("dbo.Trabajadors", "NumberHijos", c => c.Int(nullable: false));
            AlterColumn("dbo.Trabajadors", "Conyuge", c => c.String());
            AlterColumn("dbo.Trabajadors", "DiasPago", c => c.Int(nullable: false));
        }
    }
}
