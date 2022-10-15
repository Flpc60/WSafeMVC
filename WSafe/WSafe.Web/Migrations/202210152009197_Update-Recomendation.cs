namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRecomendation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reasons", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reasons", "Name", c => c.String());
        }
    }
}
