namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "NumeroTrabajadores", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "NumeroTrabajadores", c => c.Int(nullable: false));
        }
    }
}
