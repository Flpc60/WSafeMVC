using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class CapacitationRepository : GenericRepository<Capacitation>, ICapacitationRepository
    {
        public CapacitationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
