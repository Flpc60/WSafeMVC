using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class RiesgoRepository : GenericRepository<Riesgo>, IRiesgoRepository
    {
        public RiesgoRepository(Web.Models.EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}