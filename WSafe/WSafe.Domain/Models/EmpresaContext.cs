using System.Data.Entity;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Auditor;

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
        public DbSet<Control> Controles { get; set; }
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

        public System.Data.Entity.DbSet<WSafe.Web.Models.UserViewModel> UserViewModels { get; set; }

        public System.Data.Entity.DbSet<WSafe.Web.Models.RoleUserVM> RoleUserVMs { get; set; }
    }
}