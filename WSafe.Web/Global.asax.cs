using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using System.Data.Entity;
using System.Web.Optimization;
using System.Web.Routing;

namespace WSafe.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // ========= 1) DI/IoC =========
            // Si usas Bootstrapper para registrar todo, COMENTA este bloque y deja sólo Bootstrapper.Initialise();
            var container = new UnityContainer();
            // container.RegisterType<IMiServicio, MiServicio>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // ========= 2) MVC Plomería (una sola vez cada uno) =========
            AreaRegistration.RegisterAllAreas();                             // SOLO UNA VEZ
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);       // Filtros
            RouteConfig.RegisterRoutes(RouteTable.Routes);                   // Rutas - SOLO UNA VEZ
            BundleConfig.RegisterBundles(BundleTable.Bundles);               // Bundles

            // ========= 3) EF Migration to latest =========
            // Ajusta el namespace de Configuration si es diferente
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    WSafe.Domain.Data.EmpresaContext,
                    Migrations.Configuration>());   // o WSafe.Domain.Migrations.Configuration

            // ========= 4) Bootstrapper (opcional) =========
            // IMPORTANTE: Asegúrate que Bootstrapper.Initialise() NO vuelva a llamar RegisterRoutes ni RegisterAllAreas.
            // Úsalo sólo para registrar dependencias en el contenedor.
            Bootstrapper.Initialise();

            // ========= 5) Producción/Optimización =========
            BundleTable.EnableOptimizations = true;
        }
    }
}
