using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class AuditService : GenericService<Audit>, IAuditService
    {
        public AuditService(IAuditRepository auditRepository) : base(auditRepository)
        {
        }
    }
}