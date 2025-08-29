using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class AccionRepository : GenericRepository<Accion>, IAccionRepository
    {
        public AccionRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}