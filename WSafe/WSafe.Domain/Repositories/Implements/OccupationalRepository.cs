using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class OccupationalRepository : GenericRepository<Occupational>, IOccupationalRepository
    {
        public OccupationalRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}

