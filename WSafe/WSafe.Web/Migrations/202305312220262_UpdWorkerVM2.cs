namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdWorkerVM2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkersVMs", "Tratamiento", c => c.String());
            DropColumn("dbo.WorkersVMs", "Tratamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkersVMs", "Tratamento", c => c.String());
            DropColumn("dbo.WorkersVMs", "Tratamiento");
        }
    }
}
