using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class IncidenteRepository : GenericRepository<Incidente>, IIncidenteRepository
    {
        public IncidenteRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}