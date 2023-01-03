namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNorma44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normas", "Range", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normas", "Range");
        }
    }
}
