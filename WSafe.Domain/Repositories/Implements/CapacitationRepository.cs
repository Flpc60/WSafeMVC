using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class CapacitationRepository : GenericRepository<Capacitation>, ICapacitationRepository
    {
        public CapacitationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
