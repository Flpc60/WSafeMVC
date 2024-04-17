using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class ScheduleService : GenericService<Schedule>, IScheduleService
    {
        public ScheduleService(IScheduleRepository scheduleRepository) : base(scheduleRepository)
        {
        }
    }
}