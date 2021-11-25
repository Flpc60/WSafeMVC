namespace WSafe.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    using WSafe.Domain.Data.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<WSafe.Domain.Data.EmpresaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.EmpresaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}