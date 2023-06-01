namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorkerVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "Profesion", c => c.String());
            AddColumn("dbo.Trabajadors", "WorkArea", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "TipoJornada", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "TipoSangre", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Conyuge", c => c.String());
            AddColumn("dbo.Trabajadors", "NumberHijos", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "StratumCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Trabajadors", "TenenciaVivienda", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "Enfermedad", c => c.String());
            AddColumn("dbo.Trabajadors", "Tratamento", c => c.String());
            AddColumn("dbo.Trabajadors", "SpecialRecomendations", c => c.String());
            AddColumn("dbo.WorkersVMs", "Profesion", c => c.String());
            AddColumn("dbo.WorkersVMs", "WorkArea", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "TipoJornada", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "TipoSangre", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "Conyuge", c => c.String());
            AddColumn("dbo.WorkersVMs", "NumberHijos", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "StratumCategory", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.WorkersVMs", "TenenciaVivienda", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "Enfermedad", c => c.String());
            AddColumn("dbo.WorkersVMs", "Tratamento", c => c.String());
            AddColumn("dbo.WorkersVMs", "SpecialRecomendations", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkersVMs", "SpecialRecomendations");
            DropColumn("dbo.WorkersVMs", "Tratamento");
            DropColumn("dbo.WorkersVMs", "Enfermedad");
            DropColumn("dbo.WorkersVMs", "TenenciaVivienda");
            DropColumn("dbo.WorkersVMs", "Email");
            DropColumn("dbo.WorkersVMs", "StratumCategory");
            DropColumn("dbo.WorkersVMs", "NumberHijos");
            DropColumn("dbo.WorkersVMs", "Conyuge");
            DropColumn("dbo.WorkersVMs", "TipoSangre");
            DropColumn("dbo.WorkersVMs", "TipoJornada");
            DropColumn("dbo.WorkersVMs", "WorkArea");
            DropColumn("dbo.WorkersVMs", "Profesion");
            DropColumn("dbo.Trabajadors", "SpecialRecomendations");
            DropColumn("dbo.Trabajadors", "Tratamento");
            DropColumn("dbo.Trabajadors", "Enfermedad");
            DropColumn("dbo.Trabajadors", "TenenciaVivienda");
            DropColumn("dbo.Trabajadors", "Email");
            DropColumn("dbo.Trabajadors", "StratumCategory");
            DropColumn("dbo.Trabajadors", "NumberHijos");
            DropColumn("dbo.Trabajadors", "Conyuge");
            DropColumn("dbo.Trabajadors", "TipoSangre");
            DropColumn("dbo.Trabajadors", "TipoJornada");
            DropColumn("dbo.Trabajadors", "WorkArea");
            DropColumn("dbo.Trabajadors", "Profesion");
        }
    }
}
