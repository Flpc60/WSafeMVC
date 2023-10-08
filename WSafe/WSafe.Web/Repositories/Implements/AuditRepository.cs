using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class AuditRepository : GenericRepository<Audit>, IAuditRepository
    {
        public AuditRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}