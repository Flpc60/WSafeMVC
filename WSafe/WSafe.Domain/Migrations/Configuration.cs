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
            context.Trabajadores.AddOrUpdate(x => x.ID,
                new Trabajador() { ID = 1, PrimerApellido = "Puerta", SegundoApellido = "Cardona", PrimerNombre = "Francisco", Documento = "71579486" },
                new Trabajador() { ID = 2, PrimerApellido = "Bernal", SegundoApellido = "Gomez", PrimerNombre = "Patricia", Documento = "43033005" },
                new Trabajador() { ID = 3, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "David", Documento = "1039460078" },
                new Trabajador() { ID = 4, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "Sebastian", Documento = "103942525" },
                new Trabajador() { ID = 5, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "Andrea", Documento = "1000415244" }
                );
        }
    }
}