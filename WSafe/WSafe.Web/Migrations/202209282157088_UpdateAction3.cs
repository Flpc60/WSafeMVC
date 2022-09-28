namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAction3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccionViewModels", "ActionState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccionViewModels", "ActionState");
        }
    }
}
