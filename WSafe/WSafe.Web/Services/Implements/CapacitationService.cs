using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class CapacitationService : GenericService<Capacitation>, ICapacitationService
    {
        public CapacitationService(ICapacitationRepository capacitationRepository) : base(capacitationRepository)
        {
        }
    }
}