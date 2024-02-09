namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdOccupational : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreateOccupationalVMs", "FileName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Occupationals", "FileName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Occupationals", "FileName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.CreateOccupationalVMs", "FileName", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
