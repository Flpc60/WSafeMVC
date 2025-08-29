using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities.Ppre;

namespace WSafe.Domain.Repositories.Implements
{
    public class InterventionRepository : GenericRepository<Intervention>, IInterventionRepository
    {
        public InterventionRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
