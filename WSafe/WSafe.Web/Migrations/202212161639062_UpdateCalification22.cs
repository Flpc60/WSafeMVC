namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalification22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Califications", "Cumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "NoCumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "NoAplica", c => c.Boolean(nullable: false));
            AddColumn("dbo.Califications", "Valoration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Evaluations", "Cumple", c => c.Int(nullable: false));
            AlterColumn("dbo.Evaluations", "NoCumple", c => c.Int(nullable: false));
            AlterColumn("dbo.Evaluations", "NoAplica", c => c.Int(nullable: false));
            DropColumn("dbo.Califications", "Valor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Califications", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Evaluations", "NoAplica", c => c.String(maxLength: 3));
            AlterColumn("dbo.Evaluations", "NoCumple", c => c.String(maxLength: 3));
            AlterColumn("dbo.Evaluations", "Cumple", c => c.String(maxLength: 3));
            DropColumn("dbo.Califications", "Valoration");
            DropColumn("dbo.Califications", "NoAplica");
            DropColumn("dbo.Califications", "NoCumple");
            DropColumn("dbo.Califications", "Cumple");
        }
    }
}
