using System.Data.Entity;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Data
{
    class EmpresaContext : DbContext
    {
        public DbSet<Riesgo> Riesgos { get; set; }
    }
}