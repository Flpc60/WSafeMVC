using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
