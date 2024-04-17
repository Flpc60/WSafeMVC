using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
