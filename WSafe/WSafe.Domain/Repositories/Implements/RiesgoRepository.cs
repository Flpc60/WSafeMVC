using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class RiesgoRepository : GenericRepository<Riesgo>, IRiesgoRepository
    {
        public RiesgoRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
