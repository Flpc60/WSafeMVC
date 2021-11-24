using System.Data.Entity;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("DefaultConnection")
        {
        }

        public DbSet<Riesgo> Riesgos { get; set; }
    }
}