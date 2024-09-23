using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class OccupationalRepository : GenericRepository<Occupational>, IOccupationalRepository
    {
        public OccupationalRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}

