using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class AplicationRepository : GenericRepository<Aplicacion>, IAplicationRepository
    {
        public AplicationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}