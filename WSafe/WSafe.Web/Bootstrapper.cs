using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc3;
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
            container.RegisterType<IComboHelper, ComboHelper>();
            container.RegisterType<IConverterHelper, ConverterHelper>();
            container.RegisterType<IChartHelper, ChartHelper>();
            container.RegisterType<IGestorHelper, GestorHelper>();
            container.RegisterType<IEmergencyConverter, EmergencyConverter>();
            container.RegisterType<IIndicadorHelper, IndicadorHelper>();

            container.RegisterType<IAccionRepository, AccionRepository>();
            container.RegisterType<IActionPlanRepository, ActionPlanRepository>();
            container.RegisterType<IAnnualPlanRepository, AnnualPlanRepository>();
            container.RegisterType<IAplicationRepository, AplicationRepository>();
            container.RegisterType<IAuditRepository, AuditRepository>();
            container.RegisterType<ICalificationAmenazaRepository, CalificationAmenazaRepository>();
            container.RegisterType<ICapacitationRepository, CapacitationRepository>();
            container.RegisterType<IIncidenteRepository, IncidenteRepository>();
            container.RegisterType<IInterventionRepository, InterventionRepository>();
            container.RegisterType<IOccupationalRepository, OccupationalRepository>();
            container.RegisterType<IRecomendationRepository, RecomendationRepository>();
            container.RegisterType<IRiesgoRepository, RiesgoRepository>();
            container.RegisterType<IScheduleRepository, ScheduleRepository>();
            container.RegisterType<ITrainingRepository, TrainingRepository>();
            container.RegisterType<IVulnerabilityRepository, VulnerabilityRepository>();

            container.RegisterType<IAccionService, AccionService>();
            container.RegisterType<IActionPlanService, ActionPlanService>();
            container.RegisterType<IAnnualPlanService, AnnualPlanService>();
            container.RegisterType<IAplicationService, AplicationService>();
            container.RegisterType<IAuditService, AuditService>();
            container.RegisterType<ICalificationAmenazaService, CalificationAmenazaService>();
            container.RegisterType<ICapacitationService, CapacitationService>();
            container.RegisterType<IIncidenteService, IncidenteService>();
            container.RegisterType<IInterventionService, InterventionService>();
            container.RegisterType<IOccupationalService, OccupationalService>();
            container.RegisterType<IRecomendationService, RecomendationService>();
            container.RegisterType<IRiesgoService, RiesgoService>();
            container.RegisterType<IScheduleService, ScheduleService>();
            container.RegisterType<ITrainingService, TrainingService>();
            container.RegisterType<IVulnerabilityService, VulnerabilityService>();

            return container;
        }
    }
}