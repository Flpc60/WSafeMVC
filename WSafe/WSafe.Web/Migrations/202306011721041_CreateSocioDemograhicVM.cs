namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSocioDemograhicVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "Escolaridad", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "Escolaridad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkersVMs", "Escolaridad");
            DropColumn("dbo.Trabajadors", "Escolaridad");
        }
    }
}
