using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using WSafe.Domain.Data;
using WSafe.Domain.Helpers;
using WSafe.Domain.Helpers.Implements;
using WSafe.Domain.Repositories;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services;
using WSafe.Domain.Services.Implements;

namespace WSafe.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, EmpresaContext>();
            container.RegisterType<IRiesgoService, RiesgoService>();
            container.RegisterType<IRiesgoRepository, RiesgoRepository>();
            container.RegisterType<IComboHelper, ComboHelper>();
            container.RegisterType<IConverterHelper, ConverterHelper>();
            container.RegisterType<IGestorHelper, GestorHelper>();

            return container;
        }
    }
}