namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdCapacitation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Capacitations", "TrainingTopicID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Capacitations", "TrainingTopicID", c => c.String(nullable: false));
        }
    }
}
