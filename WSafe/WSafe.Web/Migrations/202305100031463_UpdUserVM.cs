namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdUserVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserViewModels", "RazonSocial", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserViewModels", "RazonSocial");
        }
    }
}
