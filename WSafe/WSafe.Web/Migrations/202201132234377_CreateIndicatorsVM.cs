namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIndicatorsVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateIndicatorsViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaInicial = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        IndicadorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.IndicadorViewModels", "IndicadorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndicadorViewModels", "IndicadorID", c => c.Int(nullable: false));
            DropTable("dbo.CreateIndicatorsViewModels");
        }
    }
}
