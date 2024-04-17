using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class OccupationalService : GenericService<Occupational>, IOccupationalService
    {
        public OccupationalService(IOccupationalRepository occupationalRepository) : base(occupationalRepository)
        {
        }
    }
}

