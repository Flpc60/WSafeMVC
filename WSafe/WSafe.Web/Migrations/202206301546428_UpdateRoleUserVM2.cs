namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoleUserVM2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleUserVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleUserVMs");
        }
    }
}
