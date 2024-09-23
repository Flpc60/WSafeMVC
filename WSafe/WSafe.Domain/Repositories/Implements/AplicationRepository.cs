using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class AplicationRepository : GenericRepository<Aplicacion>, IAplicationRepository
    {
        public AplicationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}