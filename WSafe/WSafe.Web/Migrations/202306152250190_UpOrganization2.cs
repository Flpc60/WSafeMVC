namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpOrganization2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Unsafeacts", "FileName", c => c.String(maxLength: 200));
            AlterColumn("dbo.UnsafeactVMs", "FileName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UnsafeactVMs", "FileName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Unsafeacts", "FileName", c => c.String(maxLength: 100));
        }
    }
}
