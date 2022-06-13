namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrabajadorVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabajadors", "Firma", c => c.String(maxLength: 20));
            AddColumn("dbo.Trabajadors", "Foto", c => c.String(maxLength: 20));
            AddColumn("dbo.Trabajadors", "Direccion", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String(maxLength: 20));
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trabajadors", "ARL", c => c.String());
            AlterColumn("dbo.Trabajadors", "AFP", c => c.String());
            AlterColumn("dbo.Trabajadors", "EPS", c => c.String());
            DropColumn("dbo.Trabajadors", "Direccion");
            DropColumn("dbo.Trabajadors", "Foto");
            DropColumn("dbo.Trabajadors", "Firma");
        }
    }
}
