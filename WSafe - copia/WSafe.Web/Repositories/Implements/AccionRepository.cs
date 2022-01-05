using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class AccionRepository : GenericRepository<Accion>, IAccionRepository
    {
        public AccionRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}