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
            var container = new UnityContainer();

            // Registros de tus dependencias
            // container.RegisterType<IMiServicio, MiServicio>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<WSafe.Domain.Data.EmpresaContext, Migrations.Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Initialise();

            BundleTable.EnableOptimizations = true;

        }
    }
}
