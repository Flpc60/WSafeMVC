namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOperation : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Operations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Component = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
