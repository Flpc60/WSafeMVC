using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class AuditRepository : GenericRepository<Audit>, IAuditRepository
    {
        public AuditRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}