﻿using System.Data.Entity;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Auditor;
using WSafe.Domain.Data.Entities.ICAM;
using WSafe.Web.Data.Entities;

namespace WSafe.Web.Models
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("DefaultConnection")
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Riesgo> Riesgos { get; set; }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<CategoriaPeligro> CategoriasPeligros { get; set; }
        public DbSet<Peligro> Peligros { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<RiesgoViewModel> RiesgoViewModels { get; set; }
        public DbSet<AccionViewModel> AccionViewModels { get; set; }
        public DbSet<IncidenteViewModel> IncidenteViewModels { get; set; }
        public DbSet<IndicadorViewModel> IndicadorViewModels { get; set; }
        public DbSet<CreateIndicatorsViewModel> CreateIndicatorsViewModels { get; set; }
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<AplicacionVM> AplicacionVMs { get; set; }
        public DbSet<Accidentado> Accidentados { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<PlanAccion> PlanesAccion { get; set; }
        public DbSet<PlanAction> PlanActions { get; set; }
        public DbSet<SeguimientoAccion> SeguimientosAccion { get; set; }
        public DbSet<Seguimiento> Seguimientos { get; set; }
        public DbSet<AuditRiesgo> AuditRiesgos { get; set; }
        public DbSet<AuditaAccion> AuditaAcciones { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleOperation> RoleOperations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Recomendation> Recomendations { get; set; }
        public DbSet<RootCause> RootCauses { get; set; }
        public DbSet<BarrierAnalice> BarrierAnalysis { get; set; }
        public DbSet<CausalAnalice> CausalAnalysis { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<EventsOrder> EventsOrders { get; set; }
        public DbSet<Movimient> Movimientos { get; set; }
        public DbSet<Calification> Califications { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Norma> Normas { get; set; }
        public DbSet<PlanActivity> PlanActivities { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Unsafeact> Unsafeacts { get; set; }
        public DbSet<SigueRecomendation> SigueRecomendations { get; set; }
        public DbSet<Patology> Patologies { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditedResult> AuditedResults { get; set; }
        public DbSet<Auditer> Auditers { get; set; }
        public DbSet<AuditItem> AuditItems { get; set; }
        public DbSet<AuditedAction> AuditedActions { get; set; }
        public DbSet<SiguePlanAnual> SigueAnnualPlans { get; set; }
        public DbSet<Occupational> Occupationals { get; set; }
        public DbSet<SigueOccupational> SigueOccupationals { get; set; }
        public DbSet<Capacitation> Capacitations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TrainingTopic> TrainingTopics { get; set; }
        public DbSet<ControlTrace> ControlTraces { get; set; }
        public DbSet<MainCause> MainCauses { get; set; }
        public System.Data.Entity.DbSet<WSafe.Web.Models.UserViewModel> UserViewModels { get; set; }
        public System.Data.Entity.DbSet<WSafe.Web.Models.RoleUserVM> RoleUserVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.MovimientVM> MovimientVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.EvaluationVM> EvaluationVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.MinimalsStandardsVM> MinimalsStandardsVMs { get; set; }
        public virtual DbSet<CalificationVM> CalificationVM { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.OrganizationVM> OrganizationVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.WorkersVM> WorkersVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.UnsafeactVM> UnsafeactVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.UnsafeactsListVM> UnsafeactsListVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.ActionsMatrixVM> ActionsMatrixVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.RecomendationListVM> RecomendationListVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.RecomendationVM> RecomendationVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.AuditedCreateVM> AuditedCreateVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.AnnualPlanVM> AnnualPlanVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.CreatePlanActivityVM> CreatePlanActivityVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.MedicalRecomendationVM> MedicalRecomendationVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.CreateOccupationalVM> CreateOccupationalVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.ListCapacitationVM> ListCapacitationVMs { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.CreateCapacitationVM> CreateCapacitationVMs { get; set; }
    }
}