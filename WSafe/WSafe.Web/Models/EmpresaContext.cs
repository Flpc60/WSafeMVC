using System.Data.Entity;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("DefaultConnection")
        {
        }

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
    }
}