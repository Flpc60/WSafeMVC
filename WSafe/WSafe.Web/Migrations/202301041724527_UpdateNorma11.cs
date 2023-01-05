namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Normas", "Range", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Normas", "Range", c => c.String(maxLength: 2));
        }
    }
}
