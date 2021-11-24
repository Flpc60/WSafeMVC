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

        protected override void Seed(WSafe.Domain.Data.EmpresaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Procesos.AddOrUpdate(x => x.ID,
                new Proceso() { ID = 1, Descripcion = "Administrativos" },
                new Proceso() { ID = 2, Descripcion = "Fabricación productos" },
                new Proceso() { ID = 3, Descripcion = "Control de calidad" },
                new Proceso() { ID = 4, Descripcion = "Seguridad y salud" },
                new Proceso() { ID = 5, Descripcion = "Empacado producto terminado" }
                );

            context.Zonas.AddOrUpdate(x => x.ID,
                new Zona() { ID = 1, Descripcion = "Parqueaderos" },
                new Zona() { ID = 2, Descripcion = "Porteria" },
                new Zona() { ID = 3, Descripcion = "Información" },
                new Zona() { ID = 4, Descripcion = "Recepción" },
                new Zona() { ID = 5, Descripcion = "Admisiones" }
                );

            context.Actividades.AddOrUpdate(x => x.ID,
                new Actividad() { ID = 1, Descripcion = "Mantenimiento andamios" },
                new Actividad() { ID = 2, Descripcion = "Limpieza paredes y pisos" },
                new Actividad() { ID = 3, Descripcion = "Trabajo alturas" },
                new Actividad() { ID = 4, Descripcion = "Embaldozar" },
                new Actividad() { ID = 5, Descripcion = "Empastar" },
                new Actividad() { ID = 6, Descripcion = "Resane y pintura" },
                new Actividad() { ID = 7, Descripcion = "Adaptar zocalos" },
                new Actividad() { ID = 8, Descripcion = "Instalar puertas y vetanas" }
                );

            context.Tareas.AddOrUpdate(x => x.ID,
                new Tarea() { ID = 1, Descripcion = "Digitalizar" },
                new Tarea() { ID = 2, Descripcion = "Enchapado" },
                new Tarea() { ID = 3, Descripcion = "Laminado" },
                new Tarea() { ID = 4, Descripcion = "Elaborar historias" },
                new Tarea() { ID = 5, Descripcion = "Archivar historias" }
                );

            context.CategoriasPeligros.AddOrUpdate(x => x.ID,
                new CategoriaPeligro() { ID = 1, Descripcion = "Físicos" },
                new CategoriaPeligro() { ID = 2, Descripcion = "Quimicos" },
                new CategoriaPeligro() { ID = 3, Descripcion = "Biológicos" },
                new CategoriaPeligro() { ID = 4, Descripcion = "BioMecanicos" },
                new CategoriaPeligro() { ID = 5, Descripcion = "Condiciones seguridad" },
                new CategoriaPeligro() { ID = 6, Descripcion = "Desatres Naturales" },
                new CategoriaPeligro() { ID = 7, Descripcion = "Psicosocial" }
                );

            context.Peligros.AddOrUpdate(x => x.ID,
                new Peligro() { ID = 1, CategoriaPeligroID = 1, Descripcion = "Ruido" },
                new Peligro() { ID = 2, CategoriaPeligroID = 1, Descripcion = "Iluminación" },
                new Peligro() { ID = 3, CategoriaPeligroID = 1, Descripcion = "Vibración" },
                new Peligro() { ID = 4, CategoriaPeligroID = 1, Descripcion = "Temperaturas Extremas" },
                new Peligro() { ID = 5, CategoriaPeligroID = 1, Descripcion = "Presión atmosferica" },
                new Peligro() { ID = 6, CategoriaPeligroID = 1, Descripcion = "Radiaciones Ionizantes" },
                new Peligro() { ID = 7, CategoriaPeligroID = 1, Descripcion = "Radiaciones No Ionozantes" },
                new Peligro() { ID = 8, CategoriaPeligroID = 2, Descripcion = "Polvos orgánicos, inorgánicos" },
                new Peligro() { ID = 9, CategoriaPeligroID = 2, Descripcion = "Fibras" },
                new Peligro() { ID = 10, CategoriaPeligroID = 2, Descripcion = "Liquidos" },
                new Peligro() { ID = 11, CategoriaPeligroID = 2, Descripcion = "Gases y vapores" },
                new Peligro() { ID = 12, CategoriaPeligroID = 2, Descripcion = "Humos metálicos, no metálicos" },
                new Peligro() { ID = 13, CategoriaPeligroID = 2, Descripcion = "Material particulado" },
                new Peligro() { ID = 14, CategoriaPeligroID = 3, Descripcion = "Virus" },
                new Peligro() { ID = 15, CategoriaPeligroID = 3, Descripcion = "Baterias" },
                new Peligro() { ID = 16, CategoriaPeligroID = 3, Descripcion = "Hongos" },
                new Peligro() { ID = 17, CategoriaPeligroID = 3, Descripcion = "Ricketsias" },
                new Peligro() { ID = 18, CategoriaPeligroID = 3, Descripcion = "Parasitos" },
                new Peligro() { ID = 19, CategoriaPeligroID = 3, Descripcion = "Picaduras" },
                new Peligro() { ID = 20, CategoriaPeligroID = 3, Descripcion = "Mordeduras" },
                new Peligro() { ID = 21, CategoriaPeligroID = 3, Descripcion = "Fluidos o escrementos" },
                new Peligro() { ID = 22, CategoriaPeligroID = 4, Descripcion = "Postura" },
                new Peligro() { ID = 23, CategoriaPeligroID = 4, Descripcion = "Esfuerzo" },
                new Peligro() { ID = 24, CategoriaPeligroID = 4, Descripcion = "Movimiento repetitivo" },
                new Peligro() { ID = 25, CategoriaPeligroID = 4, Descripcion = "Manipulación manual de cargas" },
                new Peligro() { ID = 26, CategoriaPeligroID = 5, Descripcion = "Mecánico" },
                new Peligro() { ID = 27, CategoriaPeligroID = 5, Descripcion = "Eléctrico" },
                new Peligro() { ID = 28, CategoriaPeligroID = 5, Descripcion = "Locativo" },
                new Peligro() { ID = 29, CategoriaPeligroID = 5, Descripcion = "Tecnológico" },
                new Peligro() { ID = 30, CategoriaPeligroID = 5, Descripcion = "Publicos" },
                new Peligro() { ID = 31, CategoriaPeligroID = 5, Descripcion = "Accidentes de tránsito" },
                new Peligro() { ID = 32, CategoriaPeligroID = 5, Descripcion = "Trabajo en alturas" },
                new Peligro() { ID = 33, CategoriaPeligroID = 5, Descripcion = "Espacios confinados" },
                new Peligro() { ID = 34, CategoriaPeligroID = 6, Descripcion = "Sismo" },
                new Peligro() { ID = 35, CategoriaPeligroID = 6, Descripcion = "Terremoto" },
                new Peligro() { ID = 36, CategoriaPeligroID = 6, Descripcion = "Vendaval" },
                new Peligro() { ID = 37, CategoriaPeligroID = 6, Descripcion = "Inundación" },
                new Peligro() { ID = 38, CategoriaPeligroID = 6, Descripcion = "Derrumbe" },
                new Peligro() { ID = 39, CategoriaPeligroID = 7, Descripcion = "Precipitaciones" },
                new Peligro() { ID = 40, CategoriaPeligroID = 7, Descripcion = "Gestión organizacional" },
                new Peligro() { ID = 41, CategoriaPeligroID = 7, Descripcion = "Caracteristicas de la organización del trabajo" },
                new Peligro() { ID = 42, CategoriaPeligroID = 7, Descripcion = "Caracteristicas del grupo social del trabajo" },
                new Peligro() { ID = 43, CategoriaPeligroID = 7, Descripcion = "Condiciones de la tarea" },
                new Peligro() { ID = 44, CategoriaPeligroID = 7, Descripcion = "Interfase persona – tarea" },
                new Peligro() { ID = 45, CategoriaPeligroID = 7, Descripcion = "Jornada de trabajo" }
                );

            context.Trabajadores.AddOrUpdate(x => x.ID,
                new Trabajador() { ID = 1, PrimerApellido = "Puerta", SegundoApellido ="Cardona", PrimerNombre = "Francisco", Documento = "71579486" },
                new Trabajador() { ID = 2, PrimerApellido = "Bernal", SegundoApellido = "Gomez", PrimerNombre = "Patricia", Documento = "43033005" },
                new Trabajador() { ID = 3, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "David", Documento = "1039460078" },
                new Trabajador() { ID = 4, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "Sebastian", Documento = "103942525" },
                new Trabajador() { ID = 5, PrimerApellido = "Puerta", SegundoApellido = "Bernal", PrimerNombre = "Andrea", Documento = "1000415244" }
                );
        }
    }
}